<?php 
    $host = "localhost";
    $dbuser = "root";
    $dbpassword = "1218";
    $dbname = "hrapp";

    $conn = new mysqli($host,$dbuser,$dbpassword,$dbname);

    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }

    $sql = "SELECT * FROM staffinfo";
    $result = mysqli_query($conn, $sql);

    if (mysqli_num_rows($result) > 0) {
        while($row = mysqli_fetch_assoc($result)) {
            echo $row["staff_id"].",".$row["name"].",".$row["account"].",".$row["pwd"].",".$row["identify"].",".$row["ondate"].",".$row["salaryType"].",".$row["dailyWage"].",".$row["timeWage"]."\n";
        }
    } else {
        echo "0 results";
    }

    $conn->close();

?>
