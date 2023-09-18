<%@ Page Title="Generate Sitemap" Language="C#" MasterPageFile="~/main-admin.master" AutoEventWireup="true" CodeFile="generate-sitemap.aspx.cs" Inherits="adm_generate_sitemap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page">
        <header class="relative pt-4">
            <div class="container-fluid ">
                <div class="row p-t-b-10 ">
                    <div class="col">
                        <h4 class="text-center">
                            <i class="icon-add"></i>
                            Generate Sitemap
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

                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-lg-12 text-center">
                                                    <h5><strong>Job Sitemap</strong> <a href="https://www.agovtjobs.in/sitemap-job.xml" target="_blank">View</a></h5>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Number of links</strong>
                                                    <asp:TextBox ID="job_numbers" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6 text-center">
                                                    <asp:Label ID="job_errorlbl" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label><br />
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div style="position: fixed; top: 0; left: 0; background-color: black; z-index: 99; opacity: 0.8; filter: alpha(opacity=80); -moz-opacity: 0.8; min-height: 100%; width: 100%;">
                                                                <p class="text-center pt-5" style="color:#fff"><br /><br /><br /><br /><strong>Please wait while we generate sitemap for you. <br />This may take upto few minutes. <br />Kindly do not close or refresh this window.</strong></p>
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:Button ID="generate_job_sitemap" OnClick="generate_job_sitemap_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Update Job Sitemap" />&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-lg-12 text-center">
                                                    <h5><strong>Admit Cards Sitemap</strong> <a href="https://www.agovtjobs.in/sitemap-admit-card.xml" target="_blank">View</a></h5>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Number of links</strong>
                                                    <asp:TextBox ID="admit_card_numbers" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6 text-center">
                                                    <asp:Label ID="admit_card_errorlbl" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label><br />
                                                    <asp:Button ID="admit_card_btn" OnClick="admit_card_btn_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Update Admit Card Sitemap" />&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-lg-12 text-center">
                                                    <h5><strong>Answer Keys Sitemap</strong> <a href="https://www.agovtjobs.in/sitemap-answer-key.xml" target="_blank">View</a></h5>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Number of links</strong>
                                                    <asp:TextBox ID="answer_key_numbers" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6 text-center">
                                                    <asp:Label ID="answer_key_errorlbl" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label><br />
                                                    <asp:Button ID="answer_key_btn" OnClick="answer_key_btn_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Update Answer Key Sitemap" />&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-lg-12 text-center">
                                                    <h5><strong>Exam Dates Sitemap</strong> <a href="https://www.agovtjobs.in/sitemap-exam-date.xml" target="_blank">View</a></h5>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Number of links</strong>
                                                    <asp:TextBox ID="exam_date_numbers" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6 text-center">
                                                    <asp:Label ID="exam_date_errorlbl" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label><br />
                                                    <asp:Button ID="exam_dates_btn" OnClick="exam_dates_btn_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Update Exam Date Sitemap" />&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-lg-12 text-center">
                                                    <h5><strong>Interview Schedules Sitemap</strong> <a href="https://www.agovtjobs.in/sitemap-interview-schedule.xml" target="_blank">View</a></h5>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Number of links</strong>
                                                    <asp:TextBox ID="interview_schedule_numbers" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6 text-center">
                                                    <asp:Label ID="interview_schedule_errorlbl" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label><br />
                                                    <asp:Button ID="interview_schedule_btn" OnClick="interview_schedule_btn_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Update Interview Schedule Sitemap" />&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-lg-12 text-center">
                                                    <h5><strong>Qualification Sitemap</strong> <a href="https://www.agovtjobs.in/sitemap-qualification.xml" target="_blank">View</a></h5>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Number of links</strong>
                                                    <asp:TextBox ID="qualification_numbers" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6 text-center">
                                                    <asp:Label ID="qualification_errorlbl" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label><br />
                                                    <asp:Button ID="qualification_btn" OnClick="qualification_btn_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Update Qualification Sitemap" />&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-lg-12 text-center">
                                                    <h5><strong>Result Sitemap</strong> <a href="https://www.agovtjobs.in/sitemap-result.xml" target="_blank">View</a></h5>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Number of links</strong>
                                                    <asp:TextBox ID="result_numbers" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6 text-center">
                                                    <asp:Label ID="result_errorlbl" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label><br />
                                                    <asp:Button ID="result_btn" OnClick="result_btn_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Update Result Sitemap" />&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-lg-12 text-center">
                                                    <h5><strong>Syllabus Sitemap</strong> <a href="https://www.agovtjobs.in/sitemap-syllabus.xml" target="_blank">View</a></h5>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Number of links</strong>
                                                    <asp:TextBox ID="syllabus_numbers" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6 text-center">
                                                    <asp:Label ID="syllabus_errorlbl" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label><br />
                                                    <asp:Button ID="syllabus_btn" OnClick="syllabus_btn_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Update Syllabus Sitemap" />&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-lg-12 text-center">
                                                    <h5><strong>Tags Sitemap</strong> <a href="https://www.agovtjobs.in/sitemap-tag.xml" target="_blank">View</a></h5>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Number of links</strong>
                                                    <asp:TextBox ID="tag_numbers" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6 text-center">
                                                    <asp:Label ID="tag_errorlbl" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label><br />
                                                    <asp:Button ID="tags_btn" OnClick="tags_btn_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Update Tags Sitemap" />&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-body b-b">
                                    <div class="form-material">
                                        <div class="body">
                                            <div class="row clearfix">
                                                <div class="col-lg-12 text-center">
                                                    <h5><strong>Written Marks Sitemap</strong> <a href="https://www.agovtjobs.in/sitemap-written-mark.xml" target="_blank">View</a></h5>
                                                </div>
                                                <div class="col-lg-6">
                                                    <strong style="color: black; font-weight: 800">Number of links</strong>
                                                    <asp:TextBox ID="written_mark_numbers" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6 text-center">
                                                    <asp:Label ID="written_mark_errorlbl" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label><br />
                                                    <asp:Button ID="written_marks_btn" OnClick="written_marks_btn_Click" CssClass="btn btn-sm btn-dark" runat="server" Text="Update Written Marks Sitemap" />&nbsp;
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
