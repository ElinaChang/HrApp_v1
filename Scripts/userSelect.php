<?php
    // 建立MySQL的資料庫連接 
    $host = "localhost";
    $dbuser = "root";
    $dbpassword = "1218";
    $dbname = "hrapp";
    $connect = new mysqli($host,$dbuser,$dbpassword,$dbname);

    $sql = "select * from staffinfo";

    $result = mysqli_query($connect, $sql);

    if(mysqli_num_rows($result)>0)
    {
        /* while ($row = mysqli_fetch_assoc($result))
        {
            echo "username: ".$row['name']."|account: ".$row['account']."|password: ".$row['pwd']."|identify: ".$row['identify'].";";
        } */

        while ($array = mysqli_fetch_array($result))
        {
            echo $array['name']."</next>";
            echo $array['account']."</next>";
            echo $array['pwd']."</next>";
            echo $array['identify']."</next>";
            /* echo $array['ondate']."</next>";
            echo $array['salary']."</next>";
            echo $array['dailyWage']."</next>";
            echo $array['timeWage']."</next>"; */
        }
    }
?>