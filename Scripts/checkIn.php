<?php
    $host = "localhost";
    $dbuser = "root";
    $dbpassword = "1218";
    $dbname = "hrapp";

    $conn = new mysqli($host,$dbuser,$dbpassword,$dbname);
    if ($conn->connect_error) 
    {
        die("連線失敗" . $conn->connect_error);
    }

    $staff_id_sql = "SELECT log_staff_id FROM log_record ORDER BY login_Time DESC";
    $result = mysqli_query($conn, $staff_id_sql);

    //Check In
    if (mysqli_num_rows($result) > 0) 
    {
        if ($row = mysqli_fetch_assoc($result)) 
        {
            $emp_id = $row["log_staff_id"];
            $checkin_time = date("Y-m-d H:i:s");
            $checkin_IP = isset($_GET['checkin_IP']) ? $_GET['checkin_IP'] : '';
            //$checkin_address = isset($_GET['checkin_address']) ? $_GET['checkin_address'] : '';
            
            $sql = "INSERT INTO ondutyrecord (emp_id, checkin_time, checkin_IP) VALUES ('$emp_id', '$checkin_time', '$checkin_IP')";
            //$sql = "INSERT INTO ondutyrecord (emp_id, checkin_time, checkin_IP, checkin_address) VALUES ('$emp_id', '$checkin_time', '$checkin_IP', '$checkin_address')";

            if ($conn->query($sql) === TRUE) 
            {
                echo "$checkin_time";
            } 
            else 
            {
                echo "Error: " . $sql . "<br>" . $conn->error;
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

    mysqli_close($conn);

?>
