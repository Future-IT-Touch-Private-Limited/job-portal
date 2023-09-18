<%@ Page Title="" Language="C#" MasterPageFile="~/main-admin.master" AutoEventWireup="true" CodeFile="delete-image.aspx.cs" Inherits="adm_delete_image" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- past orders -->
    <div class="tab-pane fade show active" id="orders" role="tabpanel" aria-labelledby="orders-tab">
        <h4 class="font-weight-bold mt-0 mb-4">Delete Image</h4>

        <div class="bg-white card mb-4 order-list shadow-sm">
            <div class="gold-members p-4">
                       <p class="text-center"><center><asp:Image ID="Image1" runat="server" Width="100px" Height="100px" /></center></p>
            </div>
        </div>

    </div>
    <br />
    <br />
    <p class="text-center">
        <asp:Label ID="confirmlbl" runat="server" Text="Are you sure you want to delete this Image?" ForeColor="Red" Font-Bold="true" Font-Size="Large"></asp:Label></p>
    <p class="text-center">
        <asp:Label ID="errorlbl" runat="server" Visible="false" Text="" ForeColor="Red" Font-Bold="true"></asp:Label></p>
    <p class="text-center">
        <asp:Button ID="deletebtn" class="btn btn-sm btn-primary" OnClick="deletebtn_Click" runat="server" Text="Yes Delete" /></p>

</asp:Content>

