using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.IO;
using System.Windows.Forms;

namespace AudioPlayer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Un4seen.Bass.BassNet.Registration("mixailkovalev167@mail.ru", "2X22297242238");
            switch(args.Length)
            {
                case 1:
                    if (args[0].ToLower().Equals("radio"))
                    {
                        OneInstanceApp.Run(new Form2(), StartupNextInstanceHandler);
                    }
                    else if (Directory.Exists(args[0]))
                    {
                        OneInstanceApp.Run(new Form1(args[0]), StartupNextInstanceHandler);
                    }
                    else if (File.Exists(args[0]))
                    {
                        OneInstanceApp.Run(new Form1(args[0], true), StartupNextInstanceHandler);
                    }
                    else
                    {
                        OneInstanceApp.Run(new Form1(), StartupNextInstanceHandler);
                    }
                    break;
                case 2:
                    if (args[0].ToLower().Equals("radio"))
                    {
                        if (Directory.Exists(args[1]))
                        {
                            OneInstanceApp.Run(new Form2(args[1]), StartupNextInstanceHandler);
                        }
                        else if (File.Exists(args[1]))
                        {
                            OneInstanceApp.Run(new Form2(args[1], true), StartupNextInstanceHandler);
                        }
                        else
                        {
                            OneInstanceApp.Run(new Form2(), StartupNextInstanceHandler);
                        }
                    }
                    else
                    {
                        OneInstanceApp.Run(new Form1(), StartupNextInstanceHandler);
                    }
                    break;
                default:
                    OneInstanceApp.Run(new Form1(), StartupNextInstanceHandler);
                    break;
            }
        }

        static void StartupNextInstanceHandler(object sender, StartupNextInstanceEventArgs e)
        {
            switch(e.CommandLine.Count)
            {
                case 2:
                    foreach (Form item in Application.OpenForms)
                    {
                        if (item is Form1)
                        {
                            if (Directory.Exists(e.CommandLine[1]))
                            {
                                CommonInterface.GetFilesFromFolder(e.CommandLine[1]);
                            }
                            else if (File.Exists(e.CommandLine[1]))
                            {
                                CommonInterface.AddTrackOrURL(e.CommandLine[1]);
                            }
                            break;
                        }
                    }
                    break;
                case 3:
                    if (e.CommandLine[1].ToLower().Equals("radio"))
                    {
                        foreach (Form item in Application.OpenForms)
                        {
                            if (item is Form2)
                            {
                                if (Directory.Exists(e.CommandLine[2]))
                                {
                                    CommonInterface.GetFilesFromFolder(e.CommandLine[2], true);
                                }
                                else if (File.Exists(e.CommandLine[2]))
                                {
                                    CommonInterface.ReadPlayList(e.CommandLine[2], true);
                                }
                                break;
                            }
                        } 
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
