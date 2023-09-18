﻿<%@ Page Title="All Tags" Language="C#" MasterPageFile="~/main-admin.master" AutoEventWireup="true" CodeFile="tags.aspx.cs" Inherits="adm_tags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page">
        <header class="relative pt-4">
            <div class="container-fluid ">
                <div class="row p-t-b-10 ">
                    <div class="col">
                        <h4 class="text-center">
                            <i class="icon-tags"></i>
                            All Tags
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

                        <div class="col-md-12">
                            <div class="card r-0 no-b shadow2">
                                <div class="shadow p-4 bg-light">
                                    <div class="align-items-center justify-content-between">
                                        <div class="row">
                                            <div class="col-lg-12">






                                                <br />
                                                <br />
                                                <%--field 16--%>
                                                <asp:panel runat="server" id="particularpanel16">
                                                    <div class="row">
                                                        <div class="form2 col-md-12">
                                                            <strong>Qualification</strong>
                                                            <asp:TextBox ID="valuetxt" TabIndex="41" Style="font-weight: 600" class="form-control" placeholder="Type here..." runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                </asp:panel>

                                                <asp:panel id="errorpanel" visible="false" backcolor="Red" runat="server">
                                                    <div class="alert alert-danger alert-dismissible mb-2" role="alert">
                                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                            <span aria-hidden="true" style="color: white">x</span>
                                                        </button>
                                                        <asp:Label ID="errorlbl" runat="server" Font-Bold="true" ForeColor="White" Text="Error"></asp:Label>
                                                    </div>
                                                </asp:panel>
                                                <asp:panel id="listfullpanel" visible="false" runat="server">
                                                    <div class="alert alert-danger alert-dismissible mb-2" role="alert">
                                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                            <span aria-hidden="true">x</span>
                                                        </button>
                                                        <asp:Label ID="listfullerrorlbl" Font-Bold="true" ForeColor="White" runat="server" Text="Error"></asp:Label>
                                                    </div>
                                                </asp:panel>

                                                <asp:panel runat="server" id="Panel16">
                                                    <br />
                                                    <div class="row">
                                                        <div class="form2 col-md-6">
                                                        </div>
                                                        <div class="form2 col-md-6">
                                                            <asp:Button ID="savebtn" OnClick="savebtn_Click" TabIndex="44" CssClass="btn btn-raised gradient-purple-bliss white shadow-z-1-hover" runat="server" Text="Save" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <br />
                                                </asp:panel>

                                                <div class="row">
                                                    <div class="col-md-12">

                                                        <table class="table table-bordered table-hover data-tables" data-options='{"searching":true}'>
                                                            <thead>
                                                                <tr class="no-b">
                                                                    <td class="bolder">
                                                                        <span style="color: black">&nbsp;Tags</span>
                                                                    </td>
                                                                    <td class="bolder">
                                                                        <span style="color: black">&nbsp;Delete</span>
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:repeater id="Repeater1" runat="server" onitemcommand="Repeater1_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr class="black-text">
                                                                            <td>
                                                                                <asp:Label ID="deletesrlbl" Visible="false" runat="server" Text='<%# Eval("sr")%>'></asp:Label>
                                                                                <strong><%# Eval("tag") %></strong>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="deletebtn" CommandName="deletebtn" CssClass="btn btn-link" runat="server" Text="Delete" />
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
                            </div>
                        </div>
                        <br />

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
