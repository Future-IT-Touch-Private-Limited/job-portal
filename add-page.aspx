<%@ Page Title="Add Page" Language="C#" MasterPageFile="~/main-admin.master" AutoEventWireup="true" CodeFile="add-page.aspx.cs" Inherits="adm_add_page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src='https://cdn.tiny.cloud/1/0zpf29a6coq36804ljgwyzugne2piwf0nqcfmm1kms1tcdv6/tinymce/5/tinymce.min.js'></script>
    <script type="text/javascript">
        tinymce.init({
            selector: "textarea",  // change this value according to your HTML
            content_style: "div {margin: 10px; border: 5px solid red; padding: 3px}",
            plugins: ["advlist autolink lists charmap preview hr anchor",
               "pagebreak code nonbreaking table contextmenu directionality paste link fullscreen image imagetools media insertdatetime searchreplace textcolor wordcount textpattern"],
            toolbar1: "styleselect | bold italic underline | pagebreak | undo redo | link | searchreplace",
            toolbar2: "alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | forecolor backcolor | fullscreen",
            relative_urls: false,
            remove_script_host: false,
            convert_urls: true,
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="page">
        <header class="relative pt-4">
            <div class="container-fluid ">
                <div class="row p-t-b-10 ">
                    <div class="col">
                        <h4 class="text-center">
                            <i class="icon-add"></i>
                            Add Page
                        </h4>
                    </div>
                </div>
            </div>
        </header>
    </div>



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
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <strong style="color: black">Page Title</strong>
                                                    <asp:TextBox ID="meta_title" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                </div>
                                                <div class="col-lg-12">
                                                    <strong style="color: black">Meta Description</strong>
                                                    <asp:TextBox ID="meta_description" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                </div>
                                                <div class="col-lg-12">
                                                    <strong style="color: black">Meta Keywords</strong>
                                                    <asp:TextBox ID="meta_keywords" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                </div>
                                                <div class="col-lg-12">
                                                    <strong style="color: black">Page URL</strong>
                                                    <asp:TextBox ID="page_short_url" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                </div>
                                                <div class="col-lg-12">
                                                    <strong style="color: black">Body</strong>
                                                    <asp:TextBox ID="body" TextMode="MultiLine" Rows="20" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                </div>
                                                <div class="col-lg-12"><br />
                                                    <strong style="color: black">Body 2</strong>
                                                    <asp:TextBox ID="body2" TextMode="MultiLine" Rows="20" CssClass="form-control" runat="server"></asp:TextBox><br />
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
                                            
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <strong style="color: black; font-weight: 800">Select State</strong>
                                                            <asp:DropDownList ID="state" OnSelectedIndexChanged="state_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control">
                                                                <asp:ListItem>Select State..</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <strong style="color: black; font-weight: 800">Select District</strong>
                                                            <asp:DropDownList ID="district" OnSelectedIndexChanged="district_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control">
                                                                <asp:ListItem>Select District..</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <strong style="color: black; font-weight: 800">Select Area</strong>
                                                            <asp:DropDownList ID="area" OnSelectedIndexChanged="area_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control">
                                                                <asp:ListItem>Select Area..</asp:ListItem>
                                                            </asp:DropDownList>
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
                                                    <strong style="color: black; font-weight: 800">Select Main Category</strong>
                                                    <asp:TextBox ID="main_category" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Select Secondry Category</strong>
                                                    <asp:TextBox ID="sub_category" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Industry</strong>
                                                    <asp:TextBox ID="industry" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Post Name</strong>
                                                    <asp:TextBox ID="post_name" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>




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
                                                    <strong style="color: black">Thumbnail Image URL</strong>
                                                    <asp:TextBox ID="thumbnail_url" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <a href="#" class="btn btn-link text-center">View All Thumbnail Images</a>
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
                                                    <strong style="color: black">Select Type</strong><br />
                                                    <asp:DropDownList ID="post_type" CssClass="form-control" runat="server">
                                                        <asp:ListItem>Admit Card</asp:ListItem>
                                                        <asp:ListItem>Result</asp:ListItem>
                                                        <asp:ListItem>News</asp:ListItem>
                                                        <asp:ListItem>Blogs</asp:ListItem>
                                                        <asp:ListItem>Syllabus</asp:ListItem>
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

                    <div class="col-lg-12 text-center">
                        <br />
                        <asp:Button ID="publish_page" OnClick="publish_page_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Publish Page" />&nbsp;
                        <asp:Button ID="draft_page" OnClick="draft_page_Click" CssClass="btn btn-sm btn-default" runat="server" Text="Send to Draft" /><br />
                    </div>

                </div>

                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
</asp:Content>