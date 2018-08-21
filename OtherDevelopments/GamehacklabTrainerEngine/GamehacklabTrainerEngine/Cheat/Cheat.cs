using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GamehacklabTrainerEngine.Core;

namespace GamehacklabTrainerEngine.Cheat
{

    class Cheat
    {

        public delegate void Enabled(Cheat cheat);
        public delegate void Disabled(Cheat cheat);

        private String Name = ""; // "Бесконечное здоровье"
        private String signature = ""; //"0B 74 EE xx DD EF EB xx xx xx 00 DF"
        private Keys vKey = 0; //VK_NUM1;
        private byte[] originalBytes; // байты оригинальной инструкции
        private IntPtr caveAddress = IntPtr.Zero; // адрес выделенной для чита памяти
        private IntPtr instructionAddress = IntPtr.Zero;
        private byte[] patchBytes; // байты для кейва
        private byte originalSize = 0; // размер оригинальной инструкции
        private bool isEnabled;
        private Thread vKeyPressDetectThread;
        private bool isRunning = false;

        public event Enabled onEnabled;
        public event Disabled onDisabled;


        public Cheat(String CheatName, String CheatSignature, Keys VirtualKey, ref byte[] patchBytes, byte originalSize)
        {
            Name = CheatName;
            signature = CheatSignature;
            vKey = VirtualKey;
            this.patchBytes = patchBytes;
            this.originalSize = originalSize;



            vKeyPressDetectThread = new Thread(this.vKeyThreadRoutine);
            isRunning = true;
            vKeyPressDetectThread.Start();

        }

        public String GetName()
        {
            return Name;
        }

        public bool Enable()
        {
            /* 1. Выделить память под кейв
             * 2. Записать байты кейва
             * 3. Высчитать байты для прыжка из кейва обратно в оригинальный код
             * 4. Высчитать байты для прыжка из оригинального кода в кейв
             * 5. Записать байты обратного прыжка в кейв
             * 6. Записать байты прыжка в кейв в оригинальную инструкцию, добив недостающие байты длины оригинальной инструкции нопами.
            */
            caveAddress = WinAPIWrapper.AllocMem(patchBytes.Length + 5);
            byte[] caveData = new byte[patchBytes.Length + 5];
            Array.Copy(patchBytes, 0, caveData, 0, patchBytes.Length);
            byte[] returnJump = CalculateJumpOpcode(caveAddress.ToInt32(), instructionAddress.ToInt32() + originalSize);
            Array.Copy(returnJump, 0, caveData, patchBytes.Length, returnJump.Length);
            WinAPIWrapper.WriteMem(caveAddress, caveData);
            byte[] jmpBytes = CalculateJumpOpcode(instructionAddress.ToInt32(), caveAddress.ToInt32());
            if (originalSize > 5)
            {
                byte[] jmpFromOriginal = new byte[originalSize];
                Array.Copy(jmpBytes, 0, jmpFromOriginal, 0, jmpBytes.Length);
                for (int i = 5; i < jmpFromOriginal.Length; i++)
                {
                    jmpFromOriginal[i] = 0x90;
                }
                WinAPIWrapper.WriteMem(instructionAddress, jmpFromOriginal);
            }
            else
            {
                WinAPIWrapper.WriteMem(instructionAddress, jmpBytes);
            }
            isEnabled = true;
            onEnabled.Invoke(this);
            return true;
        }
        public bool Disable()
        {
            isEnabled = false;
            onDisabled.Invoke(this);
            return true;
        }


        private void vKeyThreadRoutine()
        {
            while (isRunning)
            {

                if (WinAPIWrapper.GetAsyncKeyState(vKey) != 0)
                {
                    if (isEnabled) Disable();
                    else Enable();
                }

                Thread.Sleep(200);
            }
        }

        private byte[] CalculateJumpOpcode(int from, int to)
        {
            byte[] jumpOpcodes =  new byte[5];
            jumpOpcodes[0] = 0xE9;
            int result = to - from;
            byte[] opcodes = BitConverter.GetBytes(result);
            for (int i = 0; i < opcodes.Length; i++)
            {
                jumpOpcodes[i + 1] = opcodes[i];
            }

            return jumpOpcodes;

        }

    }
}
