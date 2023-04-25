
<?php
$host = "localhost";
$dbuser = "root";
$dbpassword = "1218";
$dbname = "hrapp";

$conn = new mysqli($host, $dbuser, $dbpassword, $dbname);

// 檢查連接是否成功
if ($conn->connect_error) 
{
    die("Connection failed: " . $conn->connect_error);
}

// 取得 POST 參數
$staff_id = $_POST["staff_id"];

// 構造 SQL 語句
$sql = "DELETE FROM staffinfo WHERE staff_id='$staff_id'";

// 執行 SQL 語句
if ($conn->query($sql) === TRUE) 
{
    echo "Record deleted successfully";
} 
else 
{
    echo "Error deleting record: " . $conn->error;
}

// 關閉資料庫連接
$conn->close();
?>

