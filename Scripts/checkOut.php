<?php
    $host = "localhost";
    $dbuser = "root";
    $dbpassword = "1218";
    $dbname = "hrapp";
    $conn = new mysqli($host,$dbuser,$dbpassword,$dbname);
    if ($conn->connect_error){
        die("連線失敗" . $conn->connect_error);
    }

    $log_staff_id_sql = "SELECT log_staff_id FROM log_record ORDER BY login_Time DESC LIMIT 1";
    $result_logStaffId = mysqli_query($conn, $log_staff_id_sql);

    if (mysqli_num_rows($result_logStaffId) > 0) 
    {
        if ($row = mysqli_fetch_assoc($result_logStaffId)) 
        {
            $log_staff_id = $row["log_staff_id"];

            // 查詢特定員工最新一筆出勤記錄的ID
            $query_rd = "SELECT emp_id FROM ondutyrecord WHERE emp_id = '$log_staff_id' AND checkout_time IS NULL ORDER BY checkin_time DESC LIMIT 1";
            $result_rd = mysqli_query($conn, $query_rd);
            /* $stmt = mysqli_prepare($conn, $query_rd);
            mysqli_stmt_bind_param($stmt, "i", $emp_id);
            mysqli_stmt_execute($stmt);
            $result_rd = mysqli_stmt_get_result($stmt); */

            // 如果找到了記錄，更新簽退時間
            if ($row = mysqli_fetch_assoc($result_rd)) 
            {
                $emp_id = $row["log_staff_id"];
                
                // 更新簽退時間為當前時間
                $checkout_time = date('Y-m-d H:i:s');
                $checkout_IP = isset($_GET['checkout_IP']) ? $_GET['checkout_IP'] : '';
                $query_new = "UPDATE ondutyrecord SET checkout_time = '$checkout_time', checkout_IP = '$checkout_IP' WHERE emp_id = '$emp_id'";
                
                if ($conn->query($query_new) === TRUE)
                {
                    echo "$checkout_time";
                } 
                else{
                    echo "Error: " . $query_new . "<br>" . $conn->error;
                }
            }
            else 
            {
                echo "No onduty record found";
            }
        }
        else
        {
            echo "insert checkout fail";
        }
    }
    else
    {
        echo "No staff found";
    }


    /* 
    if (mysqli_num_rows($result) > 0) 
    {
        if ($row = mysqli_fetch_assoc($result)) 
        {
            $emp_id = $row["log_staff_id"];

            $checkin_Time_sql = "SELECT checkin_Time FROM ondutyrecord WHERE emp_id = '$emp_id'";
            $checkin_Time_result = mysqli_query($conn, $checkin_Time_sql);
            $checkin_Time_row = mysqli_fetch_assoc($checkin_Time_result);
            $checkin_Time = $checkin_Time_row['checkin_Time'];

            $checkout_time = date("Y-m-d H:i:s");
            $checkout_IP = isset($_GET['checkout_IP']) ? $_GET['checkout_IP'] : '';
            //$checkout_address = isset($_GET['checkout_address']) ? $_GET['checkout_address'] : '';

            $sql = "UPDATE ondutyrecord 
                    SET checkout_time = '$checkout_time', checkout_IP = '$checkout_IP' 
                    WHERE emp_id = '$emp_id' AND DATE(checkin_Time) = '$checkin_Time'";//
            //$sql = " UPDATE ondutyrecord SET checkout_time = '$checkout_time', checkout_IP = '$checkout_IP', checkout_address = '$checkout_address' WHERE emp_id = '$emp_id' ";
            
            if ($conn->query($sql) === TRUE){
                echo "$checkout_time";
            } 
            else{
                echo "Error: " . $sql . "<br>" . $conn->error;
            }
        }
        else{
            echo "insert checkout fail";
        }
    }
    else{
        echo "No staff found";
    } */

    mysqli_close($conn);

?>
