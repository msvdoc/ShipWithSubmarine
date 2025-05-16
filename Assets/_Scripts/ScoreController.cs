using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    //Ключ для получения количества записей
    private const string KEY_COUNT = "Count"; 
    //Количество отображаемых записей
    private const int COUNT_VIEW_RECORDS = 10;
    //Ссылка на текстовое поле, где отображаются записи
    [SerializeField] private TextMeshProUGUI _saveDataText;
    //Ссылка на поле ввода имени
    [SerializeField] private TextMeshProUGUI _inputText;
    //Ссылка на текстовое поле с набранными очками.
    [SerializeField] private TextMeshProUGUI _newScoreText;
    //Ссылка на родительский объект элементов для сохранения
    [SerializeField] private GameObject _savePanel;
    //Массив куда копируются сохранённые записи.
    private string[] _scoreList;



    void Start()
    {
        //Выводим набранное количество очков
        _newScoreText.text = Score.GetScore().ToString();
        SetArray(); //Заполняю массив сохранёнными данными
        ViewScore(); //Отображаю записи
        //Отбражаю элементы управления для сохранения записи.
        ViewSavePanel(); 
    }



    private void ViewSavePanel()
    {
        int count = PlayerPrefs.GetInt(KEY_COUNT);

        if (count > COUNT_VIEW_RECORDS)
        {
            count = COUNT_VIEW_RECORDS;
        }

        string record = PlayerPrefs.GetString((count - 1).ToString());
        string[] fieldRec = record.Split();

        if (Score.GetScore() > 0)  //Если очков набрано больше 0 
        {
            //Если количество записей < 10 или очки больше очков в 10-ой записи.
            if(count < COUNT_VIEW_RECORDS || Score.GetScore() > int.Parse(fieldRec[0]))
            {
                _savePanel.SetActive(true);
            }  
        }
    }


    private void CreateData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Count", 6);
        PlayerPrefs.SetString("0", "20 Иван");
        PlayerPrefs.SetString("1", "120 Юра");
        PlayerPrefs.SetString("2", "19 Ира");
        PlayerPrefs.SetString("3", "55 Вася");
        PlayerPrefs.SetString("4", "41 Гриша");
        PlayerPrefs.SetString("5", "11 Сева");
    }



    public void ReloadData()
    {
        PlayerPrefs.DeleteAll();
    }


    public void SaveOneRecord()
    {
        int count = 0;

        if (PlayerPrefs.HasKey(KEY_COUNT))
        {
            count = PlayerPrefs.GetInt(KEY_COUNT);
        }
        //Увеличиваю общее количество записей на 1
        count++; 
        //Формирую сохраняемую строку.
        string strSave = Score.GetScore() + " " + _inputText.text;
        //Сохраняю количество записей 
        PlayerPrefs.SetInt(KEY_COUNT, count);
        //Сохраняю саму запись (напр. "121 Витя")
        PlayerPrefs.SetString((count - 1).ToString(), strSave);
        PlayerPrefs.Save(); //Сохраняю добавленную запись
        SaveSortedRecords(); //Сохраняю отсортированные данные.
        ViewScore(); //Отображаю очки.
        _savePanel.SetActive(false); //Отключаю элементы управления для сохранения имени и очков.
    }


    private void SaveSortedRecords()
    {
        SetArray(); //Заполняю массив сохранёнными данными.
        BubleSort(); //Сортирую данные методом пузырьковой сортировки.

        for (int i = 0; i < _scoreList.Length; i++)
        {
            PlayerPrefs.SetString(i.ToString(), _scoreList[i]);
        }

        PlayerPrefs.Save();
    }



    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }


    private void ViewScore()
    {
        _saveDataText.text = "";


        for (int i = 0; i < _scoreList.Length; i++)
        {
            //Заполняю текстовое поле сохранёнными данными.
            _saveDataText.text += _scoreList[i] + "\n";
        }
    }


    private void SetArray()
    {
        int count = PlayerPrefs.GetInt(KEY_COUNT);
        _scoreList = new string[count];

        for (int i = 0; i < count; i++)
        {
            _scoreList[i] = PlayerPrefs.GetString(i.ToString());
        }
    }


    private void BubleSort()
    {
        string resCur; //Переменная для хранения текущей строки массива.

        for (int j = 0; j < _scoreList.Length - 1; j++)
        {
            for (int i = 0; i < _scoreList.Length - j - 1; i++)
            {
                //Делит текущую строку на подстроки, разделителем является пробел.
                string[] rec1 = _scoreList[i].Split();
                int num1 = int.Parse(rec1[0]);//Преобразую строку в целое число.
                //Делит следующую строку на подстроки, разделителем является пробел.
                string[] rec2 = _scoreList[i + 1].Split();
                int num2 = int.Parse(rec2[0]);//Преобразую строку в целое число.

                //Если количество очков во 2-ой строке больше, чем в первой меняю строки местами.
                if (num2 > num1)
                {
                    resCur = _scoreList[i];
                    _scoreList[i] = _scoreList[i + 1];
                    _scoreList[i + 1] = resCur;
                }
            }
        }
    }


}
