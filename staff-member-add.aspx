<%@ Page Title="Add Staff Member" Language="C#" MasterPageFile="~/main-admin.master" AutoEventWireup="true" CodeFile="staff-member-add.aspx.cs" Inherits="adm_staff_member_add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="page">
        <header class="relative pt-4">
            <div class="container-fluid ">
                <div class="row p-t-b-10 ">
                    <div class="col">
                        <h4 class="text-center">
                            <i class="icon-add"></i>
                            Add New Staff Member
                        </h4>
                    </div>
                </div>
            </div>
        </header>

        <div class="container-fluid relative animatedParent animateOnce p-0">
            <div class="row no-gutters">
                <div class="col-md-12">
                    <div class="pl-3 pr-3 my-3">
                        <div class="row">
                            <div class="col-md-3"></div>
                            <div class="col-md-6">
                                <div class="card r-0 no-b shadow2">
                                    <div class="shadow p-4 bg-light">
                                        <div class="align-items-center justify-content-between">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <strong>Name *</strong>
                                                    <asp:TextBox ID="name" runat="server" CssClass="form-control" placeholder="Staff Member Name"></asp:TextBox><br />
                                                </div>
                                                <div class="col-lg-12">
                                                    <strong>Enter Username (Min. 8 Digits Required, No Whitespace)</strong>
                                                    <asp:TextBox ID="username" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox><br />
                                                </div>
                                                <div class="col-lg-12">
                                                    <strong>New Password (Min. 8 Digits Required, No Whitespace)</strong>
                                                    <asp:TextBox ID="password" runat="server" TextMode="Password" CssClass="form-control" placeholder="New Password"></asp:TextBox><br />
                                                </div>
                                                <div class="col-lg-12">
                                                    <strong>Confirm Password</strong>
                                                    <asp:TextBox ID="confirmpassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirm Password"></asp:TextBox><br />
                                                </div>
                                                <div class="col-lg-12 text-center">
                                                    <asp:Panel ID="errorpanel" runat="server" CssClass="alert alert-danger" role="alert" Visible="False">
                                                        <asp:Label ID="errorlbl" runat="server" Text=""></asp:Label>
                                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                            <span aria-hidden="true">&times;</span>
                                                        </button>
                                                    </asp:Panel>
                                                    <br />
                                                    <asp:Button ID="generateaccountbtn" OnClick="generateaccountbtn_Click" CssClass="btn btn-success btn-lg" runat="server" Text="Generate Account" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3"></div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>