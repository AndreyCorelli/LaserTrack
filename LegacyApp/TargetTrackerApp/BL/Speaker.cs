using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TargetTracker;

namespace TargetTrackerApp.BL
{
    enum SpokenWord
    {
        Старт, Стоп, Камера1, Камера2, Камера3, Камера4, Финал
    }

    class Speaker
    {
        private static Speaker instance;
        public static Speaker Instance
        {
            get { return instance ?? (instance = new Speaker()); }
        }

        private readonly Dictionary<SpokenWord, string> wordFile =
            new Dictionary<SpokenWord, string>
                {
                    {SpokenWord.Старт, "start.wav"},
                    {SpokenWord.Стоп, "stop.wav"},
                    {SpokenWord.Камера1, "cam1.wav"},
                    {SpokenWord.Камера2, "cam2.wav"},
                    {SpokenWord.Камера3, "cam3.wav"},
                    {SpokenWord.Камера4, "cam4.wav"},
                    {SpokenWord.Финал, "final.wav"}
                };
        private readonly Dictionary<SpokenWord, System.Media.SoundPlayer> players =
            new Dictionary<SpokenWord, System.Media.SoundPlayer>();
        
        private Speaker()
        {
            var pathToFiles = string.Format("{0}\\wav\\", ExecutablePath.ExecPath);
            foreach (SpokenWord wrd in Enum.GetValues(typeof(SpokenWord)))
            {
                var fileName = pathToFiles + wordFile[wrd];
                if (!File.Exists(fileName)) continue;
                var player = new System.Media.SoundPlayer(fileName);
                player.Load();
                players.Add(wrd, player);
            }
        }

        public void SayAsynch(SpokenWord wrd)
        {
            ThreadPool.QueueUserWorkItem(SpeakWordSynch, wrd);
        }

        private void SpeakWordSynch(object obWrd)
        {
            var wrd = (SpokenWord) obWrd;
            System.Media.SoundPlayer player;
            if (!players.TryGetValue(wrd, out player)) return;
            player.Play();
        }
    }
}
