<?php
	// 建立MySQL的資料庫連接 
	$host = "localhost";
	$dbuser = "root";
	$dbpassword = "1218";
	$dbname = "hrapp";
	
	$connect = new mysqli($host,$dbuser,$dbpassword,$dbname);
	
	/* if ( $link ) 
	{
		echo "連結資料庫成功<br>";
	}
	else
	{
		echo "不正確連接資料庫 " . mysqli_connect_error();
		echo "<br>";
		exit();
	}
	
	mysqli_query($link, "SET NAMES utf8");
	mysqli_query($link, "SET CHARACTER_SET_database= utf8");
	mysqli_query($link, "SET CHARACTER_SET_CLIENT= utf8");
	mysqli_query($link, "SET CHARACTER_SET_RESULTS= utf8");
	$sql = "select * from staffinfo";
	
	$result = mysqli_query($link, $sql);
	
	if (!$result) 
	{
		echo "{$sql} 語法執行失敗，錯誤訊息 " . mysqli_error($link);
		echo "<br>";
		mysqli_close($link);  // 關閉資料庫連接
		exit();
	}
	
	$rowNum = mysqli_num_rows($result);
	if ($rowNum <= 0)
	{
		echo "沒有資料";
		echo "<br>";
		mysqli_close($link);  // 關閉資料庫連接
		exit();
	}
	
	while($array = mysqli_fetch_array($result))
	{
		echo $array["name"]."</next>";
		echo $array["account"]."</next>";
		echo $array["pwd"]."</next>";
		echo $array["identify"]."</next>";
	}
	
	mysqli_free_result($result);
	
	mysqli_close($link);  // 關閉資料庫連接 */
	
?>