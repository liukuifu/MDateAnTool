<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="MdataAn.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MData统计</title>
    <link  rel="icon" href="./Images/md.ico" type="image/x-icon" />
    <link  href="./Images/md.ico" rel="SHORTCUT ICON" type="image/x-icon" />
    <link  href="./Images/md.ico" rel="Bookmark" type="image/x-icon" />
    
    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="/Scripts/bootstrap/css/bootstrap-datetimepicker.min.css" rel="stylesheet" media="screen" />
    <link href="/LXUI/css/lxui.css" rel="stylesheet" media="screen" />
</head>
<body>
<div class="container-fluid">
	<div class="row row-fluid">
		<div class="col-md-12" style="height:50px">
		</div>
	</div>
	<div class="row row-fluid">
		<div class="col-md-12">
            <input type="hidden" id="hdmsg" value="<%=strMsg%>"" /><br/>
		</div>
	</div>
	<div class="row row-fluid">
		<div class="col-md-2">
		</div>
		<div class="col-md-8">
			<nav class="navbar navbar-default" role="navigation">
				<div class="container-fluid">
					<div class="navbar-header">
						 <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-11"> <span class="sr-only">响应菜单</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> <a class="navbar-brand" href="/index.aspx">MData</a>
					</div>					
					<div class="navbar-collapse collapse" id="bs-example-navbar-collapse-11">
						<ul class="nav navbar-nav">
							<li class="active">
								<a href="#">日统计</a>
							</li>
							<li>
								<a href="/weekwf.aspx">周统计</a>
							</li>
							<li >
								<a href="/channelwf.aspx">渠道统计</a>
							</li>
						</ul>
					</div>
					
				</div>
				
			</nav>
		</div>
		<div class="col-md-4">
		</div>
	</div>
    <form id="form1" runat="server">
	<div class="row row-fluid">
	    <div class="row row-fluid">
		    <div class="col-md-12" style="height:10px">
		    </div>
	    </div>
	    <div class="row row-fluid">
		<div class="col-md-2">
		</div>
		<div class="col-md-2" style="height:50px;">
            <div class="container">
                <fieldset>
			        <div class="form-group">
                        <label for="dtp_input2" class="col-md-1 control-label" style="width:120px">开始日期 ： </label>
                        <div class="input-group date form_date col-md-2" id="datetimepicker" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                            <input class="form-control" size="10" type="text" value="" readonly runat="server" id="input2" name="input2" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
					        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
				        <input type="hidden" id="dtp_input2" value="" /><br/>
                    </div>
                </fieldset>
            </div>
		</div>        
		<div class="col-md-2">
            <div class="container">
			 <select class="form-control" runat="server" id="ctype" name="ctype" style="width:auto;display:inline" onchange="chg(this.value);"> 
                 <option selected>go2.0</option> 
                 <option>C#2.0</option> 
                 <option>killer2.0</option> 
                 <option>task2.0</option> 
             </select>
             <asp:TextBox class="form-control" ID="tbTaskId" name="tbTaskId" runat="server" style="width:60px;display: inline"></asp:TextBox>
                <label for="dtp_input2" class="control-label" style="display: inline">※ 多个ID时请以分好（;）分割</label>
		     <div style="display: inline" id="searchDiv">                 
                 <asp:Button CssClass="btn btn-info" ID="search" runat="server" Text="按钮" OnClick="search_Click" />
		     </div>
            </div>
		</div>
	    </div>
	</div> 
	<div class="row row-fluid">
		<div class="col-md-2">
		</div>
		<div class="col-md-2" style="height:50px;">    
            <fieldset>
			    <div class="form-group">    
                    <label for="dtp_input2" class="col-md-5 control-label"  style="width:120px">统计天数 ： </label>
                    <asp:TextBox class="form-control" ID="tbDayCount" name="tbDayCount" runat="server" style="width:60px;display: inline"></asp:TextBox>
                </div>
            </fieldset>
		</div>      
		<div class="col-md-8">
            <label for="dtp_input2" class="control-label">※ 最少1天；最多30天；不输入，默认为15天</label>
		</div>
	</div> 
	<div class="row row-fluid">
		<div class="col-md-2">
		</div>
		<div class="col-md-8">
			<h1 class="text-center">
				<asp:Label ID="lblTitle" runat="server" Text="GO"></asp:Label>				
			</h1>
		</div>
		<div class="col-md-2">
		</div>
	</div>
       
	<div class="row row-fluid">
		<div class="col-md-2">
		</div>
		<div class="col-md-8" style="vertical-align:central;text-align:center">
            <div style="vertical-align:central;text-align:center;padding-left:50px">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    style="vertical-align:central;text-align:center">
                    <Columns>
                        <asp:BoundField DataField="date" HeaderText="日期">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="count" HeaderText="总访问数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="daycount" HeaderText="新规则访问数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="new" HeaderText="访问新用户数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="secondnew" HeaderText="新用户的次日访问数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="secondnewp" HeaderText="新用户的次日留存比例" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="thirdnew" HeaderText="新用户的第三日访问数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="thirdnewp" HeaderText="新用户的第三日留存比例" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="threenew" HeaderText="新用户的三日访问数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="threenewp" HeaderText="新用户的三日留存比例" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Visible="False" 
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                    GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="date" HeaderText="日期" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="count" HeaderText="总访问数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="daycount" HeaderText="当日访问数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="task" HeaderText="task result 数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="taskp" HeaderText="task result比例" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="return" HeaderText="task result return == 0 数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="returnp" HeaderText="task result return == 0 比例" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="taskid" HeaderText="指定taskID 的 result 数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="taskidreturn" HeaderText="指定taskID 的 result return == 0 数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="taskidreturnp" HeaderText="指定taskID 的 result return == 0 比例" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />                    
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Visible="False"
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    style="vertical-align:central;text-align:center">
                    <Columns>
                        <asp:BoundField DataField="date" HeaderText="日期">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="count" HeaderText="总访问数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="daycount" HeaderText="新规则访问数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="new" HeaderText="访问新用户数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="secondnew" HeaderText="新用户的次日访问数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="secondnewp" HeaderText="新用户的次日留存比例" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="thirdnew" HeaderText="新用户的第三日访问数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="thirdnewp" HeaderText="新用户的第三日留存比例" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="threenew" HeaderText="新用户的三日访问数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="threenewp" HeaderText="新用户的三日留存比例" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="weekACount" HeaderText="DAU在一周后的存活数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="weekACountp" HeaderText="DAU在一周后的存活数比例" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="egg1user" HeaderText="egg1中存在用户数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="killuser" HeaderText="kill安装用户数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="v112" HeaderText=".112个数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="v107" HeaderText=".107个数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vother" HeaderText="其它版本个数" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
		    </div>
		</div>
		<div class="col-md-2">
		</div>
	</div>
     
    </form>
</div>
        
<script type="text/javascript" src="/Scripts/jquery-1.10.2.min.js" charset="UTF-8"></script>
<script type="text/javascript" src="/Scripts/bootstrap/js/bootstrap.min.js"></script>
<script type="text/javascript" src="/Scripts/bootstrap/js/bootstrap-datetimepicker.js" charset="UTF-8"></script>
<script type="text/javascript" src="/Scripts/bootstrap/js/locales/bootstrap-datetimepicker.fr.js" charset="UTF-8"></script>
<script lang="ja" type="text/javascript">
    $('.form_datetime').datetimepicker({
        //language:  'fr',
        weekStart: 1,
        todayBtn:  1,
		autoclose: 0,
		todayHighlight: 1,
		startView: 2,
		forceParse: 0,
        showMeridian: 1
    });
	$('.form_date').datetimepicker({
        language:  'fr',
        weekStart: 1,
        todayBtn:  1,
		autoclose: 1,
		todayHighlight: 1,
		startView: 2,
		minView: 2,
		forceParse: 0
	});
	$('.form_time').datetimepicker({
        language:  'fr',
        weekStart: 1,
        todayBtn:  1,
		autoclose: 1,
		todayHighlight: 1,
		startView: 1,
		minView: 0,
		maxView: 1,
		forceParse: 0
	});
	$(document).ready(function () {
	    var myDate = new Date()
	    //alert("111111")
	    //alert(myDate.toLocaleDateString())
	    //alert("22222")
	    var dn = myDate.toLocaleDateString()
	    var newdn = dn.replace('/', '-')
	    newdn = newdn.replace('/', '-')
	    //alert(newdn)
	    $('#datetimepicker').datetimepicker('setEndDate', newdn);
	    $('#tbTaskId').attr("disabled", "disabled");
	    //alert($("#ctype").val());
	    if ($("#ctype").val() == "task2.0") {
	        //alert("removeAttr");
	        $('#tbTaskId').removeAttr("disabled");
	    }
	    //alert($("#hdmsg").val());
	    if ($("#hdmsg").val() != "") {
	        $(".alert").alert($("#hdmsg").val())
	    }
	})

	function jsFunction() {
	    
	    $("#searchDiv").hide()
        return true
        //alert("33333")
        //if (confirm("确定添加员工吗?")) {
        //    //document.getElementById("search").setAttribute('disabled', 'disabled');
	    //    return true;
	    //}
	    //return false;
	}

	function chg(locationid) {
	    if (locationid == "task2.0") {
	        $('#tbTaskId').removeAttr("disabled");
	    } else {
	        $('#tbTaskId').attr("disabled", "disabled");
	    }
	}
</script>
</body>
</html>
