// namespace Core.Audio
// {
//     using System.Collections.Generic;
//     using System.IO;
//     using Config.Models;
//     using SDL2;
//     using System;
//
//     public class Music
//     {
//         public IntPtr Stream;
//
//         public void Play(int numOfLoops = -1)
//         {
//             SDL_mixer.Mix_PlayMusic(Stream, numOfLoops);
//             // if (SDL_mixer.Mix_PlayMusic(Stream, numOfLoops) == -1)
//             // {
//             //     throw new ApplicationException("Can't play music stream.");
//             // }
//         }
//
//         public static void Pause() => SDL_mixer.Mix_PauseMusic();
//         public static void Stop() => SDL_mixer.Mix_HaltMusic();
//         public static void Resume() => SDL_mixer.Mix_ResumeMusic();
//     }
//
//     public class Sound
//     {
//         public IntPtr Chunk;
//
//         public void Play(int numOfLoops = -1)
//         {
//             if (SDL_mixer.Mix_PlayChannel(-1, Chunk, numOfLoops) == -1)
//             {
//                 if (SDL_mixer.Mix_PlayChannel(0, Chunk, numOfLoops) == -1)
//                 {
//                     throw new ApplicationException("Can't play music file.");
//                 }   
//             }
//         }
//     }
//
//     public class AudioEngineSdl : IAudioEngine
//     {
//         private bool _isInitialized = false;
//         private readonly ApplicationSettings _settings;
//         public Dictionary<string, IntPtr> SoundsList { get; private set; }
//         public Dictionary<string, IntPtr> MusicList { get; private set; }
//
//         public AudioEngineSdl(IConfiguration configuration)
//         {
//             _settings = configuration.GetSettings();
//
//             SoundsList = new Dictionary<string, IntPtr>();
//             MusicList = new Dictionary<string, IntPtr>();
//         }
//
//         public void Init()
//         {
//             if (_isInitialized)
//             {
//                 throw new ApplicationException("Audio engine already initialized.");
//             }
//
//             if (SDL_mixer.Mix_Init(SDL_mixer.MIX_InitFlags.MIX_INIT_MP3) == -1)
//             {
//                 throw new ApplicationException("SDL mixer could not be initialized.");
//             }
//
//             int frequency = SDL_mixer.MIX_DEFAULT_FREQUENCY;
//             ushort format = SDL_mixer.MIX_DEFAULT_FORMAT;
//             int channels = SDL_mixer.MIX_DEFAULT_CHANNELS;
//             int chunkSize = 1024;
//             
//             if (SDL_mixer.Mix_OpenAudio(frequency, format, channels, chunkSize) == -1)
//             {
//                 throw new ApplicationException("SDL can't open Audio device.");
//             }
//
//             _isInitialized = true;
//         }
//
//         public void Dispose()
//         {
//             foreach (var (key, value) in MusicList)
//             {
//                 SDL_mixer.Mix_FreeMusic(value);
//                 MusicList.Remove(key);
//             }
//
//             foreach (var (key, value) in SoundsList)
//             {
//                 SDL_mixer.Mix_FreeChunk(value);
//                 SoundsList.Remove(key);
//             }
//             
//             SDL_mixer.Mix_CloseAudio();
//             SDL_mixer.Mix_Quit();
//         }
//
//         public Sound LoadSound(string file)
//         {
//             var fullPath = Path.Combine($"{Environment.CurrentDirectory}", $"Assets/Sounds/{file}.mp3");
//             var sound = new Sound();
//             
//             if (SoundsList.TryGetValue(fullPath, out var chunk))
//             {
//                 sound.Chunk = chunk;
//                 return sound;
//             }
//
//             chunk = SDL_mixer.Mix_LoadWAV(fullPath);
//             
//             if (chunk == null)
//             {
//                 throw new ApplicationException($"Error sound opening file: {fullPath}");
//             }
//
//             sound.Chunk = chunk;
//             return sound;
//         }
//
//         public Music LoadMusic(string file)
//         {
//             var fullPath = Path.Combine($"{Environment.CurrentDirectory}", $"Assets/Sounds/{file}.mp3");
//             var music = new Music();
//             
//             if (MusicList.TryGetValue(fullPath, out var stream))
//             {
//                 music.Stream = stream;
//                 return music;
//             }
//
//             stream = SDL_mixer.Mix_LoadMUS(fullPath);
//             
//             if (stream == null)
//             {
//                 throw new ApplicationException($"Error music opening file: {fullPath}");
//             }
//
//             music.Stream = stream;
//             return music;
//         }
//     }
// }