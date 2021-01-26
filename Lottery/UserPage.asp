<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title></title>
    <link href="/Content/bootstrap.css" rel="stylesheet"/>
    <script src="/Scripts/jquery-3.4.1.js"></script>
    <script src="/Scripts/bootstrap.js"></script>
</head>
<body>
    <!--人工選號-->
    <form id="idForm" method="post" action="https://localhost:44348/Home/AddSingleData">
        <div>
            <label for="emp_id">員工編號</label>
            <input name="emp_id" id="emp_id" type="text" required>
        </div>
        <%
		for i =  1 to  20
		name="sample" & i
        %>
        <div class="col-md-2">
            <input type="CHECKBOX" id="<%=name%>" name="lottery_num" value="<%=i%>">
            <label for="<%=name%>"><%=i%></label>
        </div>
        <% next	%>
        
        <div >
            <input type="submit" value="送出">
        </div>
    </form>
    <!--人工選號-->

    <!--電腦選號-->

    <br>
    <button id="btn_random">電腦選號</button>

    <!--電腦選號-->
</body>
</html>
<script type="text/javascript">
    $(document).ready(function () {
        $('input[type=checkbox]').click(function () {
            $("input[name='lottery_num']").attr('disabled', true);
            if ($("input[name='lottery_num']:checked").length >= 5) {
                $("input[name='lottery_num']:checked").attr('disabled', false);
                
            } else {
                $("input[name='lottery_num']").attr('disabled', false);
            }
            //console.log($("input[name='lottery_num:checked']"))
        });


        //------電腦選號-----
        $("#btn_random").click(function () {
            $.ajax({
                type: "POST",
                url: "https://localhost:44348/Home/AddSingleDataRandom",
                data: { random_emp_id:448},
                success: function (data) {
                    console.log(data);
                    
                    if (data.status === 'success') {
                        

                        var info = "電腦選號為:{0}、{1}、{2}、{3}、{4}";
                        var msg = String.format(info, data.responseText.lottery_num_1,
                            data.responseText.lottery_num_2,
                            data.responseText.lottery_num_3,
                            data.responseText.lottery_num_4,
                            data.responseText.lottery_num_5);
                        alert(msg);
                        window.location.href = "/";
                    } else {
                        alert(data.responseText);
                    }
                }
            });
        });
        //------電腦選號-----

    })


    $("#idForm").submit(function (e) {
        var favorite = [];
        $.each($("input[name='lottery_num']:checked"), function () {
            favorite.push($(this).val());
        });

        var yes = confirm('選擇以下數字？' + favorite);

        if (yes) {

            var form = $(this);
            var url = form.attr('action');

            $.ajax({
                type: "POST",
                url: url,
                data: form.serialize(), // serializes the form's elements.
                success: function (data) {
                    console.log(data);
                    alert(data.responseText);
                    if (data.status === 'success') {
                        window.location.href = "/";
                    }
                }
            });
            e.preventDefault(); // avoid to execute the actual submit of the form.
        } else {
            return false;
        }

        
       
    });



    //字串格式化
    String.format = function () {
        if (arguments.length == 0)
            return null;
        var str = arguments[0];
        for (var i = 1; i < arguments.length; i++) {
            var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
            str = str.replace(re, arguments[i]);
        }
        return str;
        
    };
</script>
