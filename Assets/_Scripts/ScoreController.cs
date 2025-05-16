using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    //���� ��� ��������� ���������� �������
    private const string KEY_COUNT = "Count"; 
    //���������� ������������ �������
    private const int COUNT_VIEW_RECORDS = 10;
    //������ �� ��������� ����, ��� ������������ ������
    [SerializeField] private TextMeshProUGUI _saveDataText;
    //������ �� ���� ����� �����
    [SerializeField] private TextMeshProUGUI _inputText;
    //������ �� ��������� ���� � ���������� ������.
    [SerializeField] private TextMeshProUGUI _newScoreText;
    //������ �� ������������ ������ ��������� ��� ����������
    [SerializeField] private GameObject _savePanel;
    //������ ���� ���������� ���������� ������.
    private string[] _scoreList;



    void Start()
    {
        //������� ��������� ���������� �����
        _newScoreText.text = Score.GetScore().ToString();
        SetArray(); //�������� ������ ����������� �������
        ViewScore(); //��������� ������
        //�������� �������� ���������� ��� ���������� ������.
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

        if (Score.GetScore() > 0)  //���� ����� ������� ������ 0 
        {
            //���� ���������� ������� < 10 ��� ���� ������ ����� � 10-�� ������.
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
        PlayerPrefs.SetString("0", "20 ����");
        PlayerPrefs.SetString("1", "120 ���");
        PlayerPrefs.SetString("2", "19 ���");
        PlayerPrefs.SetString("3", "55 ����");
        PlayerPrefs.SetString("4", "41 �����");
        PlayerPrefs.SetString("5", "11 ����");
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
        //���������� ����� ���������� ������� �� 1
        count++; 
        //�������� ����������� ������.
        string strSave = Score.GetScore() + " " + _inputText.text;
        //�������� ���������� ������� 
        PlayerPrefs.SetInt(KEY_COUNT, count);
        //�������� ���� ������ (����. "121 ����")
        PlayerPrefs.SetString((count - 1).ToString(), strSave);
        PlayerPrefs.Save(); //�������� ����������� ������
        SaveSortedRecords(); //�������� ��������������� ������.
        ViewScore(); //��������� ����.
        _savePanel.SetActive(false); //�������� �������� ���������� ��� ���������� ����� � �����.
    }


    private void SaveSortedRecords()
    {
        SetArray(); //�������� ������ ����������� �������.
        BubleSort(); //�������� ������ ������� ����������� ����������.

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
            //�������� ��������� ���� ����������� �������.
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
        string resCur; //���������� ��� �������� ������� ������ �������.

        for (int j = 0; j < _scoreList.Length - 1; j++)
        {
            for (int i = 0; i < _scoreList.Length - j - 1; i++)
            {
                //����� ������� ������ �� ���������, ������������ �������� ������.
                string[] rec1 = _scoreList[i].Split();
                int num1 = int.Parse(rec1[0]);//���������� ������ � ����� �����.
                //����� ��������� ������ �� ���������, ������������ �������� ������.
                string[] rec2 = _scoreList[i + 1].Split();
                int num2 = int.Parse(rec2[0]);//���������� ������ � ����� �����.

                //���� ���������� ����� �� 2-�� ������ ������, ��� � ������ ����� ������ �������.
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
