<?php
// 建立連接資料庫的 PDO 物件，這裡假設使用 MySQL 資料庫
$db = new PDO("mysql:host=localhost;dbname=hrapp;charset=utf8", "root", "1218");

// 從 GET 參數中獲取 SQL 指令
$sql = $_GET['sql'];

// 執行 SQL 指令並獲取結果
$stmt = $db->query($sql);
$rows = $stmt->fetchAll(PDO::FETCH_ASSOC);

// 將結果轉成 JSON 格式輸出
echo json_encode($rows);
?>
