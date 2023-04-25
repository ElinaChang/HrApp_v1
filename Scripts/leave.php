<?php
    $host = "localhost";
    $dbuser = "root";
    $dbpassword = "1218";
    $dbname = "hrapp";
    $conn = new mysqli($host,$dbuser,$dbpassword,$dbname);
    if ($conn->connect_error) 
    {
        die("Connection failed: ". $conn->connect_error);
    }

    $staff_id_sql = "SELECT log_staff_id FROM log_record";
    $result = mysqli_query($conn, $staff_id_sql);
    if (mysqli_num_rows($result) > 0) 
    {
        if ($row = mysqli_fetch_assoc($result)) 
        {
            $emp_id = $row['log_staff_id'];
            $lv_start = isset($_POST['lv_start']) ? $_POST['lv_start'] : null;
            $lv_end = isset($_POST['lv_end']) ? $_POST['lv_end'] : null;
            $lv_type = isset($_POST['lv_type']) ? $_POST['lv_type'] : null;
            $lv_reason = isset($_POST['lv_reason']) ? $_POST['lv_reason'] : null;

            $insert_sql = " INSERT lvrecord(emp_id, lv_start, lv_end, lv_type, lv_reason) VALUE ('$emp_id', '$lv_start', '$lv_end','$lv_type','$lv_reason') ";
            $result = mysqli_query($conn, $insert_sql);
        }
    }

    $conn->close();
?>
