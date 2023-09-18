<%@ Page Title="Add Extra Posts" Language="C#" MasterPageFile="~/main-admin.master" AutoEventWireup="true" CodeFile="add-extra-post.aspx.cs" Inherits="adm_add_extra_post" %>

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
                            Add Extra Posts
                        </h4>
                    </div>
                </div>
            </div>
        </header>
    </div>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="animatedParent animateOnce">
                <div class="container-fluid my-3">
                    <div class="row">

                        <div class="col-md-8">


                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-sm-12">

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <strong style="color: black">Page URL</strong><asp:Button ID="createcustomlink" OnClick="createcustomlink_Click" CssClass="btn btn-link" runat="server" Text="Create Custom URL" />
                                                            <asp:TextBox ID="page_short_url" Enabled="true" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <strong style="color: black; font-weight: 800">Select State</strong>
                                                            <asp:DropDownList ID="state" OnSelectedIndexChanged="state_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control">
                                                                <asp:ListItem>Select State..</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <strong style="color: black; font-weight: 800">Main Category</strong>
                                                            <asp:TextBox ID="main_category" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <strong style="color: black; font-weight: 800">Sub Category</strong>
                                                            <asp:TextBox ID="sub_category" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <br />
                                                            <strong style="color: black; font-weight: 800">Industry/Organization</strong>
                                                            <asp:TextBox ID="sch_industry" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <br />
                                                            <strong style="color: black; font-weight: 800">Post Name</strong>
                                                            <asp:TextBox ID="sch_post_name" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <br />
                                                            <strong style="color: black; font-weight: 800">No. of Posts</strong>
                                                            <asp:TextBox ID="sch_number_of_posts" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <br />
                                                            <strong style="color: black; font-weight: 800">Qualifications</strong><br />
                                                            <asp:TextBox ID="sch_qualification" class="tags-input form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <br />
                                                            <strong style="color: black; font-weight: 800">PDF URL</strong>
                                                            <asp:TextBox ID="pdf_url" class="form-control" runat="server"></asp:TextBox>
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

                        <div class="col-md-4">
                            <%-- thumbnail --%>
                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-lg-12">


                                                            <div class="text-center">
                                                                <asp:Button CssClass="btn btn-link" runat="server" ID="thumbimgauto" OnClick="thumbimgauto_Click" Text="Auto Thumb" />
                                                            </div>
                                                            <asp:TextBox ID="thumbnail_url" placeholder="Thumbnail Image URL" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <asp:Image ID="thumbnail_images_preview" Style="max-width: 250px; max-height: 250px" runat="server" />
                                                            <br />


                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- thumbnail --%>

                            
                    <div class="card">
                        <div class="card-body b-b">
                            <div class="form-material">
                                <div class="body">
                                    <div class="row clearfix">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <strong style="color: black">Select Type</strong><br />
                                                    <asp:DropDownList ID="post_type" CssClass="form-control" runat="server">
                                                        <asp:ListItem>Exam Dates</asp:ListItem>
                                                        <asp:ListItem>Interview Scheduled</asp:ListItem>
                                                        <asp:ListItem>Answer Keys</asp:ListItem>
                                                        <asp:ListItem>Written Marks</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <strong style="color: black">Add Tags</strong><br />
                                                            <asp:TextBox ID="tags" CssClass="tags-input form-control" runat="server" placeholder="Add Here.."></asp:TextBox>
                                                            <br />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12 text-center">
                                <asp:Label ID="errorlbl" ForeColor="Red" Font-Bold="true" Visible="false" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:Button ID="publish" OnClick="publish_post_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Publish Result" />&nbsp;
                        <asp:Button ID="draft" OnClick="draft_post_Click" CssClass="btn btn-sm btn-default" runat="server" Text="Send to Draft" /><br />
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>