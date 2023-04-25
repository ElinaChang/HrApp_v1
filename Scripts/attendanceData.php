<?php

    // 設定時區
    date_default_timezone_set('Asia/Taipei');

    /* // 啟用 session
    session_start(); */

    // 資料庫連線設定
    $host = 'localhost';
    $username = 'root';
    $password = '1218';
    $dbname = 'hrapp';

    // 連接資料庫
    $conn = mysqli_connect($host, $username, $password, $dbname);

    // 檢查連線是否成功
    if (!$conn) 
    {
        die("Connection failed: " . mysqli_connect_error());
    }

    // 取得當月的年份和月份
    $year = date('Y');
    $month = date('m');

    // 取得 log_record 表格中的 staff_id
    $staff_id_sql = "SELECT log_staff_id FROM log_record order by login_Time desc";
    $staff_id_result = mysqli_query($conn, $staff_id_sql);
    $data = "";

    if (mysqli_num_rows($staff_id_result) > 0) 
    {
        if ($staff_id_row = mysqli_fetch_assoc($staff_id_result)) 
        {
            $emp_id = $staff_id_row["log_staff_id"];
            
            $sql = "SELECT emp_id,checkin_time, checkout_time FROM ondutyrecord WHERE emp_id = '$emp_id'";
            $result2 = mysqli_query($conn, $sql);
            
            if (mysqli_num_rows($result2) > 0) 
            {
                while ($row = mysqli_fetch_assoc($result2)) 
                {
                    $checkin_time = $row["checkin_time"];
                    $checkout_time = $row["checkout_time"];
                    $data .= $row["checkin_time"].",".$row["checkout_time"]."\n";
                }
            } 
            else 
            {
                echo "No results found";
            }
        }
    } 
    else 
    {
        echo "No results found";
    } 

    // 關閉資料庫連線
    mysqli_close($conn);

    // 將數據傳回給 Unity
    echo $data;

?>