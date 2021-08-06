using System;
using System.Collections;
using System.IO;
using Gameplay;
using UnityEngine;
using UnityEngine.Serialization;
using Directory = System.IO.Directory;
using UnityEngine.UI;

namespace Data
{
    public class PlayerData : MonoBehaviour
    {
        [Serializable]
        public class Data
        {
            public int NumRaces;
            public int NumWins;
            public float RemainingTimeCurrent;
            public float RemainingTimeBest;
        }
        
        private string _directory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Data";
        private string _file = "PlayerData.json";
        private Data _data;

        [SerializeField] private Text numRacesText;
        [SerializeField] private Text numWinsText;
        [SerializeField] private Text remainingTimeTextCurrent;
        [SerializeField] private Text remainingTimeTextBest;

        // Start is called before the first frame update
        void Start()
        {
            ProcessData();
            ShowData();
        }

        private void ShowData()
        {
            numRacesText.text = _data.NumRaces.ToString();
            numWinsText.text = _data.NumWins.ToString();
            remainingTimeTextCurrent.text = "Actual: " + ((int) _data.RemainingTimeCurrent).ToString() + "s";
            remainingTimeTextBest.text = "Mejor: " + ((int) _data.RemainingTimeBest).ToString() + "s";
        }

        public static IEnumerator SaveToDisk(string path,string data)
        {
            yield return new WaitForSeconds(1);
            File.WriteAllText(path, data);
        }

        public void ProcessData()
        {
            _data = new Data();
            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }

            string file = _directory + Path.DirectorySeparatorChar + _file;
            string json;
            if (!File.Exists(file))
            {
                _data.NumRaces = 1;
                _data.NumWins = (EndGaneObserver.Victory ? 1 : 0);
                _data.RemainingTimeCurrent = EndGaneObserver.RemainingTime;
                _data.RemainingTimeBest = EndGaneObserver.RemainingTime;

                json = JsonUtility.ToJson(_data);
                File.Create(file);

                StartCoroutine(SaveToDisk(file, json));
            }
            else
            {
                json = File.ReadAllText(file);
                _data = JsonUtility.FromJson<Data>(json);
                _data.NumRaces += 1;
                _data.NumWins += (EndGaneObserver.Victory ? 1 : 0);
                _data.RemainingTimeCurrent = EndGaneObserver.RemainingTime;
                _data.RemainingTimeBest = (EndGaneObserver.RemainingTime > _data.RemainingTimeBest ? EndGaneObserver.RemainingTime : _data.RemainingTimeBest);
                
                json = JsonUtility.ToJson(_data);
                StartCoroutine(SaveToDisk(file, json));
            }
        }
    }
}
