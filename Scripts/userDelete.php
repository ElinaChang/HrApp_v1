<?php
include ('connectmysql.php');

$whereField = $_POST['whereField'];
$whereCondition = $_POST['whereCondition'];

$sql = " delete from staffinfo where where ".$whereField."='".$whereCondition."' ";
$result = mysqli_query($connect, $sql);

?>