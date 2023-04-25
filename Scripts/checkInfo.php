<?php
$host = 'localhost';
$username = 'root';
$password = '1218';
$dbname = 'hrapp';
$conn = mysqli_connect($host, $username, $password, $dbname);
if ($conn->connect_error) 
{
    die("Connection failed: " . $conn->connect_error);
}

    
$log_staff_id = "SELECT log_staff_id FROM log_record ORDER BY login_Time DESC";
$log_staff_id_result = mysqli_query($conn, $log_staff_id);

if (mysqli_num_rows($log_staff_id_result) > 0) 
{
    if($row = $log_staff_id_result->fetch_assoc())
    {
        $emp_id = $row['log_staff_id'];
        $sql = "SELECT MIN(checkin_time) AS earliest_checkin, 
                MIN(checkin_IP) AS earliest_checkin_IP, 
                MAX(checkout_time) AS latest_checkout, 
                MAX(checkout_IP) AS latest_checkout_IP 
                FROM ondutyrecord 
                WHERE emp_id = $emp_id AND DATE(checkin_time) = CURDATE()";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) 
        {
            $output = "";
            if ($row = $result->fetch_assoc())
            {
                $output .= $row["earliest_checkin"] . "," . $row["earliest_checkin_IP"] . "," . $row["latest_checkout"] . "," . $row["latest_checkout_IP"] . ",";
            }
            $output = rtrim($output, ",");
            echo $output;
        } 
        else 
        {
            echo "0 results";
        }
    }
}

$conn->close();
?>
