<%@ Page Title="All Staff Members" Language="C#" MasterPageFile="~/main-admin.master" AutoEventWireup="true" CodeFile="staff-member-all.aspx.cs" Inherits="adm_staff_member_all" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>

    <div class="page">
        <header class="relative pt-4">
            <div class="container-fluid ">
                <div class="row p-t-b-10 ">
                    <div class="col">
                        <h4 class="text-center">
                            <i class="icon-list2"></i>
                            All Staff Members
                        </h4>
                    </div>
                </div>
            </div>
        </header>
    </div>


    <div class="container-fluid relative animatedParent animateOnce p-0">
        <div class="row no-gutters">
            <div class="col-md-12">
                <div class="pl-3 pr-3 my-3">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:panel id="sucesspanel" runat="server" style="color: white; background: green" cssclass="alert alert-success" role="alert" visible="false">
                                <asp:Label ID="sucesslbl" runat="server" Text=""></asp:Label>
                                                        <button type="button" style="color:white" class="close" data-dismiss="alert" aria-label="Close">
                                                            <span aria-hidden="true">&times;</span>
                                                        </button>
                            </asp:panel>
                            <asp:panel id="errorpanel" style="color: white; background: red" runat="server" cssclass="alert alert-danger" role="alert" visible="false">
                                <asp:Label ID="errorlbl" runat="server" Text=""></asp:Label>
                                <button type="button" style="color:white" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </asp:panel>
                        </div>

                        <div class="col-md-12">
                            <div class="card r-0 no-b shadow2">
                                <div class="shadow p-4 bg-light">
                                    <div class="align-items-center justify-content-between">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:button id="blockaccountbtn" CssClass="btn btn-danger btn-sm" commandargument='<%#Eval("sr") %>' runat="server" text="Blocked Account" />
                                                <asp:button id="activateaccountbtn" CssClass="btn btn-success btn-sm" commandargument='<%#Eval("sr") %>' runat="server" text="Active Account" />
                                                <br />
                                                <br />
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="no-b">
                                                            <td class="bolder">
                                                                <span style="color: black">&nbsp;Username</span>
                                                            </td>
                                                            <td class="bolder">
                                                                <span style="color: black">&nbsp;Name</span>
                                                            </td>
                                                            <td class="bolder">
                                                                <span style="color: black">&nbsp;Account Status</span>
                                                            </td>
                                                            <td class="bolder">
                                                                <span style="color: black">&nbsp;Join Date</span>
                                                            </td>
                                                            <td class="bolder">
                                                                <span style="color: black">&nbsp;More</span>
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:repeater id="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr class="black-text">
                                                                    <td>
                                                                        <asp:Label ID="usernamelbl" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                                                        </td>
                                                                    <td class="w-10"><%# Eval("name") %></td>
                                                                    <td><%# Eval("status") %></td>
                                                                    <td><%# Eval("joindate") %></td>
                                                                    <td>
                                                                        <a class="btn-fab btn-fab-sm btn-primary text-white" href='#'>
                                                                            <i class="icon-share"></i>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:repeater>
                                                    </tbody>
                                                </table>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="col-lg-12">
                            <div class="table-responsive">
                                <table class="table">
                                    <tr>
                                        <td>
                                            <asp:linkbutton id="lnkFirst" runat="server" onclick="lnkFirst_Click">First</asp:linkbutton>
                                        </td>
                                        <td>
                                            <asp:linkbutton id="lnkPrevious" runat="server" onclick="lnkPrevious_Click">Previous</asp:linkbutton>
                                        </td>
                                        <td>
                                            <asp:datalist id="RepeaterPaging" runat="server"
                                                onitemcommand="RepeaterPaging_ItemCommand"
                                                onitemdatabound="RepeaterPaging_ItemDataBound" repeatdirection="Horizontal">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Pagingbtn" runat="server"
                                                        CommandArgument='<%# Eval("PageIndex") %>' CommandName="newpage"
                                                        Text='<%# Eval("PageText") %>' Width="20px"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:datalist>
                                        </td>
                                        <td>
                                            <asp:linkbutton id="lnkNext" runat="server" onclick="lnkNext_Click">Next</asp:linkbutton>
                                        </td>
                                        <td>
                                            <asp:linkbutton id="lnkLast" runat="server" onclick="lnkLast_Click">Last</asp:linkbutton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" class="text-center">
                                            <asp:label id="lblpage" runat="server" text=""></asp:label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
