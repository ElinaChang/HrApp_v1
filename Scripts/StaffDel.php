<?php
    // Get the ID from the URL
    $staff_id = $_GET["staff_id"];
    
    $host = "localhost";
    $dbuser = "root";
    $dbpassword = "1218";
    $dbname = "hrapp";
    $conn = new mysqli($host,$dbuser,$dbpassword,$dbname);
    if ($conn->connect_error) 
    {
        die("Connection failed: " . $conn->connect_error);
    }

    $sql = "DELETE FROM staffinfo WHERE staff_id=$staff_id";

    if ($conn->query($sql) === TRUE) 
    {
        echo "Data deleted successfully";
    } 
    else
    {
        echo "Error deleting data: " . $conn->error;
    }
      
      $conn->close();
?>
