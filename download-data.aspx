<%@ Page Title="Download Jobs" Language="C#" MasterPageFile="~/main-admin.master" AutoEventWireup="true" CodeFile="download-data.aspx.cs" Inherits="adm_download_data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table td
        {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="page">
                <header class="relative pt-4">
                    <div class="container-fluid ">
                        <div class="row p-t-b-10 ">
                            <div class="col">
                                <h4 class="text-center">
                                    <i class="icon-download"></i>
                                    Filter/Download Posts
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

                                <div class="col-md-6 offset-md-3">
                                    <div class="card r-0 no-b shadow2">
                                        <div class="shadow p-4 bg-light">
                                            <div class="align-items-center justify-content-between">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <strong class="text-black">Download Last</strong>
                                                        <div class="input-group mb-3">
                                                            <asp:TextBox ID="downloadlatesttxt" TextMode="Number" Style="font-weight: 600" class="form-control" runat="server"></asp:TextBox>
                                                            <asp:Button ID="viewpostbtn" class="btn btn-primary" runat="server" Text="View" OnClick="viewpostbtn_Click" />
                                                        </div>
                                                        <strong class="text-black">View By Date</strong>
                                                        <div class="input-group mb-3">
                                                            <asp:Button ID="viewsubmittedbtn" OnClick="viewsubmittedbtn_Click" class="btn btn-warning" runat="server" Text="Submitted" />
                                                            <asp:TextBox ID="viewbydatetxt" TextMode="date" Style="font-weight: 600" class="form-control" runat="server"></asp:TextBox>
                                                            <asp:Button ID="viewbydatebtn" OnClick="viewbydatebtn_Click" class="btn btn-primary" runat="server" Text="Published" />
                                                        </div>
                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                            <ProgressTemplate>
                                                                <p class="text-center"><strong class="text-black">Please wait...</strong></p>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 pt-3">
                                    <asp:Button ID="downloadlastpost" OnClick="downloadlastpost_Click" CssClass="btn btn-link" runat="server" Text="Download" /> &nbsp; <asp:Label ID="statuslbl" runat="server" Text="Showing 0 Posts"></asp:Label>
                                    <div class="table-responsive" style="background:white">
                                        <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" ForeColor="Black" runat="server">
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="downloadlastpost" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
