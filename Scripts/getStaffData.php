<?php
$host = "localhost";
$dbuser = "root";
$dbpassword = "1218";
$dbname = "hrapp";

$conn = new mysqli($host,$dbuser,$dbpassword,$dbname);

if ($conn->connect_error) 
{
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT * FROM staffinfo";
$result = $conn->query($sql);

// Create an empty array to store the staff data
$staffList = array();

// Loop through each row of the result and add the staff data to the array
if ($result->num_rows > 0) 
{
    while($row = mysqli_fetch_assoc($result)) 
    {
        $staff = array
        (
            "staff_id" => $row["staff_id"],
            "name" => $row["name"],
            /* "title" => $row["title"],
            "isAvailable" => $row["isAvailable"],
            "notes" => $row["notes"] */
        );

        array_push($staffList, $staff);
    }
}

// Convert the staff array to JSON format
$jsonString = json_encode($staffList);

// Return the JSON data
echo $jsonString;


/* if ($result->num_rows > 0) 
{
    $data = '';
  
    // Loop through each row and append it to the data string
    while ($row = $result->fetch_assoc()) 
    {
        $id = $row['staff_id'];
        $name = $row['name'];
        $data .= $id . ',' . $name . '\n';
    }
  
    // Return the data as plain text
    header('Content-Type: text/plain');
    echo $data;
} 
else 
{
    echo "No data available";
} */
  
$conn->close(); 


    /* ---------------------------暫保留code--------------------------------- */
    /* header('Content-Type: text/html; charset=utf-8');

    $host = "localhost";
    $dbuser = "root";
    $dbpassword = "1218";
    $dbname = "hrapp";
    $conn = new mysqli($host,$dbuser,$dbpassword,$dbname);
    $conn->set_charset("utf8mb4");  // 設置字元集為 utf8mb4

    if ($conn->connect_error) 
    {
        die("Connection failed: " . $conn->connect_error);
    }

    // Query database
    $sql = "SELECT staff_id, name, ondate, salaryType, dailyWage, timeWage FROM staffinfo ORDER BY staff_id";
    $result = $conn->query($sql);

    // Process results into an array of objects
    $staff = array();

    if ($result->num_rows > 0) 
    {
        while ($row = $result->fetch_assoc()) 
        {
            $staff_id = intval($row["staff_id"]);
            $name = mb_split(",", $row["name"]);  // 使用 mb_split() 分割中文字串
            $ondate = mb_split(",", $row["ondate"]);
            $salaryType = mb_split(",", $row["salaryType"]);
            $dailyWage = mb_split(",", $row["dailyWage"]);
            $timeWage = mb_split(",", $row["timeWage"]);

            // 將 Unicode 編碼轉換為中文
            foreach ($name as &$value) 
            {
                $value = json_decode('"' . $value . '"');
            }
            foreach ($salaryType as &$value) 
            {
                if ($value === "\u65e5\u85aa") 
                {
                    $value = "日薪";
                } 
                else if ($value === "\u6642\u85aa") 
                {
                    $value = "月薪";
                }
            }
            foreach ($dailyWage as &$value) 
            {
                $value = json_decode('"' . $value . '"');
            }
            foreach ($timeWage as &$value) 
            {
                $value = json_decode('"' . $value . '"');
            }

            $staffMember = new stdClass();
            $staffMember->staff_id = $staff_id;
            $staffMember->name = $name;
            $staffMember->ondate = $ondate;
            $staffMember->salaryType = $salaryType;
            $staffMember->dailyWage = $dailyWage;
            $staffMember->timeWage = $timeWage;

            array_push($staff, $staffMember);
        }
    }

    // Return JSON data
    echo json_encode($staff);

    $conn->close(); */
?>
