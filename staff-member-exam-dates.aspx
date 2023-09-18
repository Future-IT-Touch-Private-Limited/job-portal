<%@ Page Title="All Submitted Exam Dates" Language="C#" MasterPageFile="~/main-admin.master" AutoEventWireup="true" CodeFile="staff-member-exam-dates.aspx.cs" Inherits="adm_staff_member_exam_dates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                            All Submitted Exam Dates
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
                            <asp:Panel ID="sucesspanel" runat="server" style="color:white; background:green" CssClass="alert alert-success" role="alert" Visible="false">
                                <asp:Label ID="sucesslbl" runat="server" Text=""></asp:Label>
                                                        <button type="button" style="color:white" class="close" data-dismiss="alert" aria-label="Close">
                                                            <span aria-hidden="true">&times;</span>
                                                        </button>
                            </asp:Panel>
                            <asp:Panel ID="errorpanel" style="color:white; background:red" runat="server" CssClass="alert alert-danger" role="alert" Visible="false">
                                <asp:Label ID="errorlbl" runat="server" Text=""></asp:Label>
                                <button type="button" style="color:white" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </asp:Panel>
                        </div>

                        <div class="col-md-12">
                            <div class="card r-0 no-b shadow2">
                                <div class="shadow p-4 bg-light">
                                    <div class="align-items-center justify-content-between">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:Button ID="deleteitem" CommandArgument='<%#Eval("sr") %>' runat="server" Text="Delete" />
                                                <br />
                                                <br />
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="no-b">
                                                            <td class="bolder">
                                                                <span style="color: black">&nbsp;<asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" /></span>
                                                            </td>
                                                            <td class="bolder">
                                                                <span style="color: black">&nbsp;Post Type</span>
                                                            </td>
                                                            <td class="bolder">
                                                                <span style="color: black">&nbsp;Title</span>
                                                            </td>
                                                            <td class="bolder">
                                                                <span style="color: black">&nbsp;Category</span>
                                                            </td>
                                                            <td class="bolder">
                                                                <span style="color: black">&nbsp;More</span>
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="Repeater1" runat="server">
                                                            <ItemTemplate>
                                                                <tr class="black-text">
                                                                    <td>
                                                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                                                        <asp:HiddenField ID="hdnID" Value='<%#Eval("sr") %>' runat="server" />
                                                                    </td>
                                                                    <td><%# Eval("post_status") %></td>
                                                                    <td class="w-10"><%# Eval("sch_industry") %>, <%# Eval("sch_post_name") %> - <%# Eval("state") %> <br />
                                                                        <a href='<%# Eval("page_full_url") %>' style="font-size: small; color: blue" target="_blank"><%# Eval("page_full_url") %></a>
                                                                    </td>
                                                                    <td><%# Eval("main_category") %>/<br />
                                                                        <%# Eval("sub_category") %></td>
                                                                    <td>
                                                                        <a class="btn-fab btn-fab-sm btn-primary text-white" href='edit-exam-interview.aspx?id=<%# Eval("sr") %>'>
                                                                            <i class="icon-share"></i>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
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
                                            <asp:LinkButton ID="lnkFirst" runat="server" OnClick="lnkFirst_Click">First</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">Previous</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:DataList ID="RepeaterPaging" runat="server"
                                                OnItemCommand="RepeaterPaging_ItemCommand"
                                                OnItemDataBound="RepeaterPaging_ItemDataBound" RepeatDirection="Horizontal">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Pagingbtn" runat="server"
                                                        CommandArgument='<%# Eval("PageIndex") %>' CommandName="newpage"
                                                        Text='<%# Eval("PageText") %>' Width="20px"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">Next</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkLast" runat="server" OnClick="lnkLast_Click">Last</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" class="text-center">
                                            <asp:Label ID="lblpage" runat="server" Text=""></asp:Label>
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