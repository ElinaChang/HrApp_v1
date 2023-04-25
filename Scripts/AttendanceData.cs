using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AttendanceData : MonoBehaviour
{
    private string phpUrl = "http://localhost/HrApp_v1/Assets/Scripts/attendanceData.php";
    public Text ondayText;
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(phpUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string[] rows = www.downloadHandler.text.Split('\n');
            int daysWorked = 0;

            for (int i = 0; i < rows.Length; i++)
            {
                string[] fields = rows[i].Split(',');
                if (fields.Length >= 2)
                {
                    string checkInTimeStr = fields[0];
                    string checkOutTimeStr = fields[1];

                    Debug.Log("行 " + i + " 字段数量：" + fields.Length + " 内容：" + rows[i]);

                    if (!string.IsNullOrEmpty(checkInTimeStr) && !string.IsNullOrEmpty(checkOutTimeStr))
                    {
                        if (DateTime.TryParseExact(checkInTimeStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime checkInTime) &&
                            DateTime.TryParseExact(checkOutTimeStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime checkOutTime))
                        {
                            if (checkInTime.Date == checkOutTime.Date)
                            {
                                daysWorked++;
                            }
                            else
                            {
                                for (DateTime d = checkInTime.Date.AddDays(1); d < checkOutTime.Date; d = d.AddDays(1))
                                {
                                    if (d.DayOfWeek >= DayOfWeek.Monday && d.DayOfWeek <= DayOfWeek.Friday)
                                    {
                                        daysWorked++;
                                    }
                                }

                                if (checkInTime.DayOfWeek >= DayOfWeek.Monday && checkInTime.DayOfWeek <= DayOfWeek.Friday)
                                {
                                    daysWorked++;
                                }

                                if (checkOutTime.DayOfWeek >= DayOfWeek.Monday && checkOutTime.DayOfWeek <= DayOfWeek.Friday)
                                {
                                    daysWorked++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("行 " + i + " 字段数量：" + fields.Length + " 内容：" + rows[i] + " 數據格式無效");
                }
            }
            Debug.Log("本月出勤天數: " + daysWorked);
            ondayText.text = daysWorked.ToString();
        }
    }
}

