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
        public static int Volume = 100;

        /// <summary>
        /// Канал остановлен пользователем
        /// </summary>
        private static bool isStoped = true;

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
        /// Инициализация bass.dll
        /// </summary>
        /// <param name="hz"></param>
        /// <returns></returns>
        public static bool InitAudio(int hz)
        {
            if (!InitDefaultDevice)
            {
                BassNet.Registration("mixailkovalev167@mail.ru", "2X22297242238");
                InitDefaultDevice = Bass.BASS_Init(-1, hz, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
                if (InitDefaultDevice)
                {
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + @"libraries\bass_aac.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + @"libraries\bass_ac3.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + @"libraries\bass_ape.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + @"libraries\bass_mpc.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + @"libraries\bass_tta.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + @"libraries\bassalac.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + @"libraries\bassflac.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + @"libraries\bassopus.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + @"libraries\basswma.dll"));
                    BassPluginsHandles.Add(Bass.BASS_PluginLoad(CommonInterface.AppPath + @"libraries\basswv.dll"));
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
            }
        }

        /// <summary>
        /// Стоп
        /// </summary>
        public static void Stop()
        {
            Visualisation.ClearPeaks();
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
                        Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Volume);
                    }
                    else if (Random)
                    {
                        Random rand = new Random();
                        CommonInterface.CurrentTrackNumber = rand.Next(CommonInterface.Files.Count);
                        if (!Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Volume))
                            return false;
                    }
                    else
                    {
                        if (!Play(CommonInterface.Files[++CommonInterface.CurrentTrackNumber].Path, Volume))
                            return false;
                    }
                    return true;
                }
                else
                {
                    if (Repeat)
                    {
                        Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Volume);
                        EndPlaylist = false;
                        return true;
                    }
                    else if (Random)
                    {
                        Random rand = new Random();
                        CommonInterface.CurrentTrackNumber = rand.Next(CommonInterface.Files.Count);
                        if (Play(CommonInterface.Files[CommonInterface.CurrentTrackNumber].Path, Volume))
                        {
                            EndPlaylist = false;
                            return true;
                        }
                        else return false;
                    }
                    else
                    {
                        EndPlaylist = true;
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
