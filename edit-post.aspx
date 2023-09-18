<%@ Page Title="Edit Post" Language="C#" MasterPageFile="~/main-admin.master" AutoEventWireup="true" CodeFile="edit-post.aspx.cs" Inherits="adm_edit_post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="updatesitemap_lbl" runat="server" Text="" Visible="true"></asp:Label>
    <div class="page">
        <header class="relative pt-4">
            <div class="container-fluid ">
                <div class="row p-t-b-10 ">
                    <div class="col">
                        <h4 class="text-center">
                            <i class="icon-add"></i>
                            Add Post
                        </h4>
                    </div>
                </div>
            </div>
        </header>
    </div>

    <asp:HiddenField ID="post_status_hf" runat="server" />
    <div class="animatedParent animateOnce">
        <div class="container-fluid my-3">
            <div class="row">

                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                <div class="col-md-8">


                    <div class="card">
                        <div class="card-body b-b">
                            <div class="form-material">
                                <div class="body">
                                    <div class="row clearfix">
                                        <div class="col-sm-12">
                                            <asp:HiddenField ID="state_hf" runat="server" />
                                            <asp:HiddenField ID="district_hf" runat="server" />
                                            <asp:HiddenField ID="area_hf" runat="server" />

                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
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
                                                            <asp:TextBox ID="statetxt" runat="server" class="form-control" placeholder="Excel State"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <strong style="color: black; font-weight: 800">Select District</strong>
                                                            <asp:DropDownList ID="district" OnSelectedIndexChanged="district_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control">
                                                                <asp:ListItem>Select District..</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="districttxt" runat="server" class="form-control" placeholder="Excel District"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <strong style="color: black; font-weight: 800">Select Area</strong>
                                                            <asp:DropDownList ID="area" OnSelectedIndexChanged="area_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control">
                                                                <asp:ListItem>Select Area..</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="areatxt" runat="server" class="form-control" placeholder="Excel Area"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <strong style="color: black; font-weight: 800">Pin Code</strong>
                                                            <asp:TextBox ID="pin_code" ReadOnly="true" BackColor="#eaeaea" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

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
                                                    <strong style="color: black; font-weight: 800">Qualifications</strong>
                                                    <asp:TextBox ID="sch_qualification" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6">
                                                    <br />
                                                    <strong style="color: black; font-weight: 800">Valid Through</strong>
                                                    <asp:TextBox ID="sch_valid_through" TextMode="Date" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6">
                                                    <br />
                                                    <strong style="color: black; font-weight: 800">PDF URL</strong>
                                                    <asp:TextBox ID="pdf_url" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6">
                                                    <br />
                                                    <strong style="color: black; font-weight: 800">Apply Now URL</strong>
                                                    <asp:TextBox ID="sch_apply_now_url" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6">
                                                    <br />
                                                    <strong style="color: black; font-weight: 800">Responsibilities/Skills</strong>
                                                    <asp:TextBox ID="sch_responsibilities" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6">
                                                    <br />
                                                    <strong style="color: black; font-weight: 800">Salery</strong>
                                                    <asp:TextBox ID="sch_salery" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
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
                                                        <asp:Button CssClass="btn btn-link" runat="server" ID="thumbimgauto" OnClick="thumbimgauto_Click" Text="Change Thumbnail" />
                                                    </div>
                                                    <strong style="color: black">Thumbnail Image URL</strong>
                                                    <asp:TextBox ID="thumbnail_url" CssClass="form-control" runat="server"></asp:TextBox>
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
                                                    <strong style="color: black">Add Tags</strong><br />
                                                    <asp:TextBox ID="tags" CssClass="tags-input form-control" runat="server" placeholder="Add Here.."></asp:TextBox>
                                                    <br />
                                                    <strong style="color: black">Schema Status</strong><br />
                                                    <asp:DropDownList ID="schema_status" CssClass="form-control" runat="server">
                                                        <asp:ListItem>Applied</asp:ListItem>
                                                        <asp:ListItem>Disabled</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <strong style="color: black">Post Status</strong><br />
                                                    <asp:DropDownList ID="post_status" CssClass="form-control" runat="server">
                                                        <asp:ListItem>Published</asp:ListItem>
                                                        <asp:ListItem>Draft</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:Label ID="last_update" runat="server" Text="Last Update: "></asp:Label><br />
                                                    <asp:Label ID="post_published" runat="server" Text="Post Published: "></asp:Label><hr />
                                                    <asp:Label ID="submitted_by_lbl" runat="server" Text="Submitted By:"></asp:Label><br />
                                                    <asp:Label ID="submitted_date_lbl" runat="server" Text="Submitted Date:"></asp:Label><br />
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
                        <asp:Label ID="successlbl" ForeColor="green" Font-Bold="true" Visible="false" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Button ID="updata_post" OnClick="updata_post_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Update Post" />&nbsp;<br />
                    </div>

                </div>


                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
</asp:Content>
