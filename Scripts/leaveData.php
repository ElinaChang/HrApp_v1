<?php
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

    // 取得 log_record 表格中的 id
    $log_staff_id = "SELECT log_staff_id FROM log_record ORDER BY login_Time DESC";
    $log_staff_id_result = mysqli_query($conn, $log_staff_id);

    if (mysqli_num_rows($log_staff_id_result) > 0) 
    {
        if ($staff_id_row = mysqli_fetch_assoc($log_staff_id_result)) 
        {
            $emp_id = $staff_id_row["log_staff_id"];
            
            // 構造查詢
            $sql = "SELECT lv_start, lv_end FROM lvrecord WHERE emp_id = '$emp_id'";

            // 執行查詢
            $result = mysqli_query($conn, $sql);

            // 檢查查詢是否成功
            if (!$result) 
            {
                die("Query failed: " . mysqli_error($conn));
            }

            // 計算請假總時數
            $total_hours = 0;
            while ($row = mysqli_fetch_assoc($result)) 
            {
                // 計算每一筆請假的時數
                $start_time = strtotime($row["lv_start"]);
                $end_time = strtotime($row["lv_end"]);
                $diff_seconds = $end_time - $start_time;
                $diff_hours = round($diff_seconds / (60 * 60), 1);

                // 加上請假時數
                $total_hours += $diff_hours;
            }

            // 轉換為請假天數
            $total_days = round($total_hours / 8, 1);

            // 返回請假天數總和
            echo $total_days;
        }
    }

    // 關閉 MySQL 連接
    mysqli_close($conn);
?>
