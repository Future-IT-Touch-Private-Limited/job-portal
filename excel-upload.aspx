<%@ Page Title="" Language="C#" MasterPageFile="~/main-admin.master" AutoEventWireup="true" CodeFile="excel-upload.aspx.cs" Inherits="excel_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .emptyRow{
            display:none;
        }
    </style>
    <div class="page">
        <header class="relative pt-4">
            <div class="container-fluid ">
                <div class="row p-t-b-10 ">
                    <div class="col">
                        <h4 class="text-center">
                            <i class="icon-tags"></i>
                            Upload Excel<br />
                            <a href="https://www.agovtjobs.in/adm/job_format.xlsx" target="_blank">Download Format</a>
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

                        <div class="col-md-6">
                            <div class="card r-0 no-b shadow2">
                                <div class="shadow p-4 bg-light">
                                    <div class="align-items-center justify-content-between">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <br />
                                                <br />
                                                <%--field 16--%>
                                                <asp:Panel runat="server" ID="particularpanel16">
                                                    <div class="row">
                                                        <div class="form2 col-md-12">
                                                            <strong>Select Excel File</strong>
                                                            <asp:FileUpload ID="FileUpload1" class="form-control" runat="server" />
                                                            <asp:Label ID="Label1" runat="server" Text="Has Header ?"></asp:Label><br />
                                                            <asp:RadioButtonList ID="rbHDR" runat="server" RepeatLayout="Flow">
                                                                <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <br />
                                                </asp:Panel>

                                                <asp:Panel ID="errorpanel" Visible="false" BackColor="Red" runat="server">
                                                    <div class="alert alert-danger alert-dismissible mb-2" role="alert">
                                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                            <span aria-hidden="true" style="color: white">x</span>
                                                        </button>
                                                        <asp:Label ID="errorlbl" runat="server" Font-Bold="true" ForeColor="White" Text="Error"></asp:Label>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel runat="server" ID="Panel16">
                                                    <br />
                                                    <div class="row">
                                                        <div class="form2 col-md-12 text-center">
                                                            <asp:Button ID="uploadbtn" OnClick="uploadbtn_Click" TabIndex="44" CssClass="btn btn-raised gradient-purple-bliss white shadow-z-1-hover" runat="server" Text="Preview Excel" />
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div class="col-lg-12" style="height:500px; overflow-y: scroll;">
                                                <div class="table-responsive">
                                                <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" EmptyDataRowStyle-Visible="false" CssClass="table table-bordered table-stripped table-hover" runat="server" OnPageIndexChanging="PageIndexChanging" AllowPaging="true" PageSize="50">
                                                    <EmptyDataRowStyle CssClass="emptyRow" />
                                                </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form2 col-md-12"><br /><br />
                                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                    <asp:Button ID="uploadjobsbtn" OnClick="uploadjobsbtn_Click" Visible="false" TabIndex="44" CssClass="btn btn-raised gradient-purple-bliss white shadow-z-1-hover" runat="server" Text="Upload Jobs" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="col-md-6">
                            <div class="card r-0 no-b shadow2">
                                <div class="shadow p-4 bg-light">
                                    <div class="align-items-center justify-content-between">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:Label ID="logsliteral" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
