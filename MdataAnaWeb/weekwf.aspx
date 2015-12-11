<%@ Page Language="C#" AutoEventWireup="true" CodeFile="weekwf.aspx.cs" Inherits="MdataAn.weekwf" %>

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
							<li>
								<a href="/index.aspx">日统计</a>
							</li>
							<li class="active">
								<a href="#">周统计</a>
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
		<div class="col-md-2">
		</div>
		<div class="col-md-2" style="height:50px;">
            <div class="container">
                <fieldset>
			        <div class="form-group">
                        <label for="dtp_input2" class="col-md-1 control-label">日 期 ： </label>
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
             </select>
		     <div style="display: inline" id="searchDiv">                 
                 <asp:Button CssClass="btn btn-info" ID="search" runat="server" Text="按钮" OnClick="search_Click" />
		     </div>
            </div>
		</div>
	</div> 
	<div class="row row-fluid">
		<div class="col-md-2">
		</div>
		<div class="col-md-8">
			<h1 class="text-center">
				<asp:Label ID="lblTitle" runat="server" Text="GO2.0"></asp:Label>				
			</h1>
		</div>
		<div class="col-md-2">
		</div>
	</div>
       
	<div class="row row-fluid">
		<div class="col-md-2">
		</div>
		<div class="col-md-8" style="vertical-align:central;text-align:center">
            <div style="vertical-align:central;text-align:center;padding-left:200px">
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Visible="True"
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    style="vertical-align:central;text-align:center;padding-right:200px">
                    <Columns>
                        <asp:BoundField DataField="week" HeaderText="自然周单位">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="240px" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="weekcount" HeaderText="本周总访问数(DAU)" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="weeknewcount" HeaderText="本周用户(DNU)" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nextweekcount" HeaderText="次周存活" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="weekACountp" HeaderText="次周存活/本周新用户" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="220px" />
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
	    //$('#tbTaskId').attr("disabled", "disabled");
	    ////alert($("#ctype").val());
	    //if ($("#ctype").val() == "task") {
	    //    //alert("removeAttr");
	    //    $('#tbTaskId').removeAttr("disabled");
	    //}
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

	//function chg(locationid) {
	//    if (locationid == "task") {
	//        $('#tbTaskId').removeAttr("disabled");
	//    } else {
	//        $('#tbTaskId').attr("disabled", "disabled");
	//    }
	//}
</script>
</body>
</html>
