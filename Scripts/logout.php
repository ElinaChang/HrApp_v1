<?php 
    $host = "localhost";
    $dbuser = "root";
    $dbpassword = "1218";
    $dbname = "hrapp";

    $conn = new mysqli($host,$dbuser,$dbpassword,$dbname);

    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }

    $log_staff_id = "SELECT log_staff_id FROM log_record ORDER BY login_Time DESC";
    $log_result = mysqli_query($conn, $log_staff_id);

    if ($log_result->num_rows > 0) {
        $row = $log_result->fetch_assoc();
        $log_staff_id = $row['log_staff_id'];
        $logout_Time = date("Y-m-d H:i:s");
        
        // update
        $sql = " UPDATE log_record SET logout_Time='".$logout_Time."' WHERE log_staff_id='".$log_staff_id."' ";
        $result = mysqli_query($conn, $sql);
        
        echo "logout time inserted".$logout_Time; 
    }else{
        echo "logout time not inserted";
    }

    $conn->close(); 
?>