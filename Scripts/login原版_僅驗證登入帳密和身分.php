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

    //接收登入帳號、密碼、身分(user or manager)
    if(isset($_POST["account"], $_POST["pwd"], $_POST["identify"]))
    {
        //trim() 函数是用來移除字串兩端的空白字符（包括空格、換行、制表符等）的
        $account = trim($_POST["account"]);
        $pwd = trim($_POST["pwd"]);
        $identify = trim($_POST["identify"]);

        //防止 SQL 注入攻擊，使用預處理語句
        $stmt = $conn->prepare("SELECT * FROM staffinfo WHERE account = ? AND pwd = ? AND identify = ?");
        $stmt->bind_param("sss", $account, $pwd, $identify);
        $stmt->execute();
        $result = $stmt->get_result();

        //判斷是否查詢到資料
        if ($result->num_rows > 0) 
        {
            echo "login success"; 
        } 
        else 
        {
            echo "login failed"; 
        }
    } 
    else 
    {
        echo "請確認帳號、密碼、登入身份無誤";
    }

    $conn->close();
?>