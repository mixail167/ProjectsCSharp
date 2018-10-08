using System;
using System.Collections.Generic;
using Un4seen.Bass;
using Un4seen.Bass.Misc;

namespace AudioPlayer
{
    public static class Audio
    {
        /// <summary>
        /// Частота дискретизации
        /// </summary>
        public static int HZ = 44100;

        /// <summary>
        /// Состояние инициализации
        /// </summary>
        public static bool InitDefaultDevice;

        /// <summary>
        /// Канал
        /// </summary>
        public static int Stream;

        /// <summary>
        /// Визуалзация канала
        /// </summary>
        public static Visuals Visualisation;

        /// <summary>
        /// Громкость
        /// </summary>
        public static int Volume;

        /// <summary>
        /// Канал остановлен пользователем
        /// </summary>
        public static bool isStoped = true;

        /// <summary>
        /// Треклист полностью проигран
        /// </summary>
        public static bool EndPlaylist;

        /// <summary>
        /// Повтор трека
        /// </summary>
        public static bool Repeat = false;

        /// <summary>
        /// Cлучайный Трек
        /// </summary>
        public static bool Random = false;

        /// <summary>
        /// Список плагинов
        /// </summary>
        private static readonly List<int> BassPluginsHandles = new List<int>();

        /// <summary>
        /// Текущий трек
        /// </summary>
        private static string CurrentTrackName = string.Empty;

        /// <summary>
        /// Дескриптор эффекта 'Хорус'
        /// </summary>
        private static int FXChorusHandle = 0;

        /// <summary>
        /// Эффект 'Хорус'
        /// </summary>
        private static BASS_DX8_CHORUS Chorus = new BASS_DX8_CHORUS(0f, 25f, 90f, 5f, 1, 0f, BASSFXPhase.BASS_FX_PHASE_NEG_90);

        /// <summary>
        /// Дескриптор эффекта 'Эхо'
        /// </summary>
        private static int FXEchoHandle = 0;

        /// <summary>
        /// Эффект 'Эхо'
        /// </summary>
        private static BASS_DX8_ECHO Echo = new BASS_DX8_ECHO(90f, 50f, 500f, 500f, false);

        /// <summary>
        /// Параметрический эквалайзер (дескрипторы)
        /// </summary>
        private static int[] FX = new int[18];

        /// <summary>
        /// Частоты
        /// </summary>
        private static int[] Frequencies = new int[18] { 31, 63, 87, 125, 175, 250, 350, 500, 700, 1000, 1400, 2000, 2800, 4000, 5600, 8000, 11200, 16000 };

        /// <summary>
        /// Дескриптор эффекта 'Предусиление'
        /// </summary>
        private static int FXVolumeHandle = 0;

        /// <summary>
        /// Эффект 'Предусиление'
        /// </summary>
        private static BASS_FX_VOLUME_PARAM VolumeFX = new BASS_FX_VOLUME_PARAM();

        /// <summary>
        /// Установка эффектов
        /// </summary>
        /// <param name="echo"></param>
        /// <param name="chorus"></param>
        /// <param name="eq"></param>
        /// <param name="save"></param>
        public static void SetEffects(float echo = 0f, float chorus = 0f, float volume = 1f, float[] eq = null, bool save = true)
        {
            Echo.fWetDryMix = echo;
            Bass.BASS_FXSetParameters(FXEchoHandle, Echo);
            Chorus.fWetDryMix = chorus;
            Bass.BASS_FXSetParameters(FXChorusHandle, Chorus);
            VolumeFX.fTarget = volume;
            VolumeFX.lCurve = 1;
            Bass.BASS_FXSetParameters(FXVolumeHandle, VolumeFX);
            BASS_DX8_PARAMEQ parameter = new BASS_DX8_PARAMEQ();
            parameter.fBandwidth = 2.5f;
            if (eq == null)
            {
                eq = new float[18];
            }
            for (int i = 0; i < FX.Length; i++)
            {
                parameter.fGain = eq[i];
                parameter.fCenter = Frequencies[i];
                Bass.BASS_FXSetParameters(FX[i], parameter);
            }
            if (save)
            {
                Properties.Settings.Default.Chorus = chorus;
                Properties.Settings.Default.Echo = echo;
                Properties.Settings.Default.VolumeFX = volume;
                Properties.Settings.Default.EQ0 = eq[0];
                Properties.Settings.Default.EQ1 = eq[1];
                Properties.Settings.Default.EQ2 = eq[2];
                Properties.Settings.Default.EQ3 = eq[3];
                Properties.Settings.Default.EQ4 = eq[4];
                Properties.Settings.Default.EQ5 = eq[5];
                Properties.Settings.Default.EQ6 = eq[6];
                Properties.Settings.Default.EQ7 = eq[7];
                Properties.Settings.Default.EQ8 = eq[8];
                Properties.Settings.Default.EQ9 = eq[9];
                Properties.Settings.Default.EQ10 = eq[10];
                Properties.Settings.Default.EQ11 = eq[11];
                Properties.Settings.Default.EQ12 = eq[12];
                Properties.Settings.Default.EQ13 = eq[13];
                Properties.Settings.Default.EQ14 = eq[14];
                Properties.Settings.Default.EQ15 = eq[15];
                Properties.Settings.Default.EQ16 = eq[16];
                Properties.Settings.Default.EQ17 = eq[17];
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Инициализация bass.dll
        /// </summary>
        /// <param name="hz"></param>
        /// <returns></returns>
        public static bool InitAudio(int hz)
        {
            if (!InitDefaultDevice)
            {
                InitDefaultDevice = Bass.BASS_Init(-1, hz, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
                if (InitDefaultDevice)
                {
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + "libraries\bass_aac.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + "libraries\bass_ac3.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + "libraries\bass_ape.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + "libraries\bass_mpc.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + "libraries\bass_tta.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + "libraries\bassalac.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + "libraries\bassflac.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + "libraries\bassopus.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + "libraries\basswma.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + "libraries\basswv.dll"));
                    Visualisation = new Visuals();
                    Visualisation.MaxFrequencySpectrum = 1100;
                }
            }
            return InitDefaultDevice;
        }

        /// <summary>
        /// Воспроизведение треков
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="volume"></param>
        public static bool Play(string filename, int volume)
        {
            if (Bass.BASS_ChannelIsActive(Stream) != BASSActive.BASS_ACTIVE_PAUSED ||
                (Bass.BASS_ChannelIsActive(Stream) == BASSActive.BASS_ACTIVE_PAUSED && !filename.Equals(CurrentTrackName)))
            {
                Stop();
                if (InitAudio(HZ) && SetStream(filename))
                {
                    Volume = volume;
                    Bass.BASS_ChannelSetAttribute(Stream, BASSAttribute.BASS_ATTRIB_VOL, Volume / 100f);
                    Bass.BASS_ChannelPlay(Stream, false);
                    CurrentTrackName = filename;
                    InitEqualizer(); 
                }
                else return false;
            }
            else
            {
                Bass.BASS_ChannelPlay(Stream, false);
            }
            isStoped = false;
            return true;
        }
        /// <summary>
        /// Инициализация эквалайзера
        /// </summary>
        private static void InitEqualizer()
        {
            FXChorusHandle = Bass.BASS_ChannelSetFX(Stream, BASSFXType.BASS_FX_DX8_CHORUS, 1);
            FXEchoHandle = Bass.BASS_ChannelSetFX(Stream, BASSFXType.BASS_FX_DX8_ECHO, 1);
            FXVolumeHandle = Bass.BASS_ChannelSetFX(Stream, BASSFXType.BASS_FX_VOLUME, 1);
            for (int i = 0; i < FX.Length; i++)
            {
                FX[i] = Bass.BASS_ChannelSetFX(Stream, BASSFXType.BASS_FX_DX8_PARAMEQ, 1);
            }
            if (Properties.Settings.Default.EQMode)
            {
                SetEffects(Properties.Settings.Default.Echo,
                            Properties.Settings.Default.Chorus, 
                            Properties.Settings.Default.VolumeFX,
                            new float[]
                                {
                                    Properties.Settings.Default.EQ0,
                                    Properties.Settings.Default.EQ1,
                                    Properties.Settings.Default.EQ2,
                                    Properties.Settings.Default.EQ3,
                                    Properties.Settings.Default.EQ4,
                                    Properties.Settings.Default.EQ5,
                                    Properties.Settings.Default.EQ6,
                                    Properties.Settings.Default.EQ7,
                                    Properties.Settings.Default.EQ8,
                                    Properties.Settings.Default.EQ9,
                                    Properties.Settings.Default.EQ10,
                                    Properties.Settings.Default.EQ11,
                                    Properties.Settings.Default.EQ12,
                                    Properties.Settings.Default.EQ13,
                                    Properties.Settings.Default.EQ14,
                                    Properties.Settings.Default.EQ15,
                                    Properties.Settings.Default.EQ16,
                                    Properties.Settings.Default.EQ17
                                }, false);
            }
            else
            {
                SetEffects(save: false);
            }
        }

        /// <summary>
        /// Воспроизведение радио
        /// </summary>
        /// <param name="url"></param>
        /// <param name="volume"></param>
        public static void PlayRadio(string url, int volume)
        {
            Stop();
            if (InitAudio(HZ) && SetStream(url, true))
            {
                Volume = volume;
                Bass.BASS_ChannelSetAttribute(Stream, BASSAttribute.BASS_ATTRIB_VOL, Volume / 100f);
                Bass.BASS_ChannelPlay(Stream, false);
                InitEqualizer();               
            }
        }

        /// <summary>
        /// Стоп
        /// </summary>
        public static void Stop()
        {
            if (Visualisation != null)
            {
                Visualisation.ClearPeaks();
            }
            Bass.BASS_ChannelStop(Stream);
            Bass.BASS_StreamFree(Stream);
            isStoped = true;
        }

        /// <summary>
        /// Пауза
        /// </summary>
        public static void Pause()
        {
            if (Bass.BASS_ChannelIsActive(Stream) == BASSActive.BASS_ACTIVE_PLAYING)
            {
                Bass.BASS_ChannelPause(Stream);
                CommonInterface.Pause = true;
            }
        }

        /// <summary>
        /// Получение длительности канала в секундах
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static int GetTimeOfStream(int stream)
        {
            long timeBytes = Bass.BASS_ChannelGetLength(stream);
            double time = Bass.BASS_ChannelBytes2Seconds(stream, timeBytes);
            return (int)time;
        }

        /// <summary>
        /// Получение текущей позиции в секундах
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static int GetPosOfStream(int stream)
        {
            long position = Bass.BASS_ChannelGetPosition(stream);
            double posSec = Bass.BASS_ChannelBytes2Seconds(stream, position);
            return (int)posSec;
        }

        /// <summary>
        /// Установка позиции в секундах
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="position"></param>
        public static void SetPosOfScroll(int stream, int position)
        {
            Bass.BASS_ChannelSetPosition(stream, (double)position);
        }

        /// <summary>
        /// Установка громкости
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="volume"></param>
        public static void SetVolumeToStream(int stream, int volume)
        {
            Volume = volume;
            Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, Volume / 100f);
        }

        /// <summary>
        /// Переход к следующему треку
        /// </summary>
        /// <returns></returns>
        public static bool ToNextTrack()
        {
            if ((Bass.BASS_ChannelIsActive(Stream) == BASSActive.BASS_ACTIVE_STOPPED) && !isStoped)
            {
                if (CommonInterface.Files.Count > CommonInterface.CurrentTrackNumber + 1)
                {
                    EndPlaylist = false;
                    if (Repeat)
                    {
                        if (Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Volume))
                            return true;
                        return false;
                    }
                    else if (Random)
                    {
                        Random rand = new Random();
                        while (CommonInterface.Files.Count > 0)
                        {
                            CommonInterface.CurrentTrackNumber = rand.Next(CommonInterface.Files.Count);
                            if (Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Volume))
                                return true;
                            else
                            {
                                CommonInterface.DeleteTrack(CommonInterface.CurrentTrackNumber);
                            }
                        }
                        return false;
                    }
                    else
                    {
                        while (CommonInterface.CurrentTrackNumber + 1 < CommonInterface.Files.Count)
                        {
                            if (Play(CommonInterface.Files[++CommonInterface.CurrentTrackNumber].Path, Volume))
                                return true;
                            else
                            {
                                CommonInterface.DeleteTrack(CommonInterface.CurrentTrackNumber);
                                CommonInterface.CurrentTrackNumber--;
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    if (Repeat)
                    {
                        if (Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Volume))
                        {
                            EndPlaylist = false;
                            return true;
                        }
                        else
                        {
                            EndPlaylist = true;
                            return false;
                        }
                    }
                    else if (Random)
                    {
                        Random rand = new Random();
                        while (CommonInterface.Files.Count > 0)
                        {
                            CommonInterface.CurrentTrackNumber = rand.Next(CommonInterface.Files.Count);
                            if (Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Volume))
                            {
                                EndPlaylist = false;
                                return true;
                            }
                            else
                            {
                                CommonInterface.DeleteTrack(CommonInterface.CurrentTrackNumber);
                            }
                        }
                        EndPlaylist = true;
                        return false;
                    }
                    else
                    {
                        EndPlaylist = true;
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Очистка 
        /// </summary>
        public static void Free()
        {
            Bass.BASS_ChannelStop(Stream);
            Bass.BASS_StreamFree(Stream);
            Bass.BASS_PluginFree(Stream);
            Bass.BASS_Free();
        }

        /// <summary>
        /// Установка канала
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isRadio"></param>
        /// <returns></returns>
        public static bool SetStream(string file, bool isRadio = false)
        {
            if (isRadio)
            {
                Stream = Bass.BASS_StreamCreateURL(file, 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);
            }
            else
            {
                Stream = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_DEFAULT);
            }
            return (Stream != 0 ? true : false);
        }
    }
}
