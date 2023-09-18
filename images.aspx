<%@ Page Title="Images" Language="C#" MasterPageFile="~/main-admin.master" AutoEventWireup="true" CodeFile="images.aspx.cs" Inherits="adm_images" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="page">
        <header class="relative pt-4">
            <div class="container-fluid ">
                <div class="row p-t-b-10 ">
                    <div class="col">
                        <h4 class="text-center">
                            <i class="icon-image"></i>
                            Upload/Copy/Delete Image
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

                            <div class="col-lg-12">
                                <div class="card r-0 no-b shadow2">
                                    <div class="shadow p-4 bg-light">
                                        <div class="align-items-center justify-content-between">
                                            <div class="row">
                                                <div class="col-lg-3">
                                                    JPG, JPEG, WEBP, PNG<br />
                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                                    <asp:Label ID="errorlbl" runat="server" ForeColor="Red" Visible="false" Text=""></asp:Label>
                                                    <br />
                                                </div>
                                                <div class="col-lg-3">
                                                    <strong>State</strong><br />
                                                    <asp:DropDownList ID="state" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-3">
                                                    <strong>Job Type</strong><br />
                                                    <asp:DropDownList ID="job_type" CssClass="form-control" runat="server">
                                                        <asp:ListItem>Govt Jobs</asp:ListItem>
                                                        <asp:ListItem>Private Jobs</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-3">
                                                    <strong>Industry</strong><br />
                                                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-12 text-center"><br />
                                                    <asp:Button ID="btnUpload" OnClick="btnUpload_Click" runat="server" CssClass="btn btn-success" Text="Upload" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <div class="col-lg-4">
                                        <div class="card r-0 no-b shadow2">
                                            <div class="shadow p-4 bg-light">
                                                <div class="align-items-center justify-content-between">
                                                    <div class="row">
                                                        <div class="col-lg-12 text-center">
                                                            <img src='<%# Eval("logourl") %>' style="width: 100%; height: 200px" />
                                                            <input type="text" class="form-control" value='<%# Eval("logourl") %>' /><br />
                                                            <a href='delete-image.aspx?sr=<%# Eval("sr") %>&file=<%# Eval("filename") %>'>Delete</a>&nbsp;<%# Eval("state") %>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>