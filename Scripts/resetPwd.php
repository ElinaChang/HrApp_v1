<?php
// 建立MySQL的資料庫連接 
$host = "localhost";
$dbuser = "root";
$dbpassword = "1218";
$dbname = "hrapp";

$conn = new mysqli($host,$dbuser,$dbpassword,$dbname);

// 檢查是否成功連接到數據庫
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}


// 從POST請求中獲取用戶提交的信息
$name = trim($_POST['name']);
$account = trim($_POST['account']);
$pwd = trim($_POST['pwd']);
$identify = trim($_POST['identify']);

// 對新密碼進行 URL 編碼，以避免破壞 SQL 語句的結構
$pwd = urlencode($pwd);

// SQL查詢語句
$sql = " update staffinfo set pwd='".$pwd."' where name='".$name."' AND account='".$account."' AND identify='".$identify."' ";


// 執行SQL查詢
if(mysqli_query($conn, $sql)) 
{
    echo "密碼更新成功";
} 
else 
{
    echo "密碼更新失敗" . mysqli_error($conn);
    //echo "Error: " . $sql . "<br>" . $conn->error;
}

// 關閉數據庫連接
mysqli_close($conn);
?>
