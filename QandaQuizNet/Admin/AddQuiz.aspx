<%@ Page Title="Add Quiz" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddQuiz.aspx.cs" Inherits="QandaQuizNet.AddQuiz" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        fieldset.scheduler-border {
            border: 1px solid #ddd !important;
            padding: 4px !important;
            margin: 5px !important;
            /*width:800px;         */
        }

        legend.scheduler-border {
            color: #1277CF;
            font-size: 20px !important;
            font-weight: bold !important;
            text-align: left !important;
            width: auto;
            /*padding:0 10px; */ /* To give a bit of padding on the left and right */
            border-bottom: none;
            margin-bottom: 5px;
        }

        .btn-file {
            position: absolute;
            z-index: 2;
            top: 0;
            left: 0;
            filter: alpha(opacity=0);
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=0)";
            opacity: 0;
            background-color: transparent;
            color: transparent;
        }

        legend {
            color: #1277CF;
            font-size: 20px !important;
            font-weight: bold !important;
            text-align: left !important;
            /*padding:0 10px; /* To give a bit of padding on the left and right */
            border-bottom: 1px dotted #1277CF;
            margin-bottom: 15px;
        }

        input[id*='AnswerText'] {
            /*border:1px solid red;*/
            display: inline;
        }
    </style>

    <script type="text/javascript" src="<%=ResolveUrl("~/scripts/moment.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/scripts/bootstrap-datetimepicker.min.js")%>"></script>

    <script type="text/javascript">
        $(function () {

            $("form")
                .attr("enctype", "multipart/form-data")
                .attr("encoding", "multipart/form-data");

            $(".chb").not(this).each(function () {
                $(this).change(function () {
                    $(".chb").not(this).attr('checked', false);
                });              
            });

             $(".chb").click(function () {
                if ($(".chb").is(":checked"))
                    $("#divUploadFile").removeClass("hidden");
                else
                    $("#divUploadFile").addClass("hidden");
             });

            //for answers
             $(".chbAns").not(this).each(function () {
                 $(this).change(function () {
                     $(".chbAns").not(this).attr('checked', false);
                 });
             });


             $("#datetimepicker2").datetimepicker({
                inline: true,
                sideBySide: true,                
                format: 'DD/MM/YYYY HH:mm'               
            });

           
            var setDate;
                       
            //in edit mode this will be set to quiz active date time
            if ($("#<%= txtquizAvtiveDateTime.ClientID%>").val() != "") {               
                setDate = moment($('#<%= txtquizAvtiveDateTime.ClientID %>').val(), "DD/MM/YYYY HH:mm");
            }
            else {
                setDate = moment();
            }
           
            $("#datetimepicker2").data("DateTimePicker")
                .date(setDate)
                //.minDate(setDate)                
                .stepping(30);
           

           <%--$("#<%= txtquizAvtiveDateTime.ClientID%>").val(moment($("#datetimepicker2").data("DateTimePicker").date()).format('DD/MM/YYYY HH:mm'));--%>

            //on change of date, set hidden field to selected date time
            $("#datetimepicker2").on("dp.change", function (e) {
                var m = moment($("#datetimepicker2").data("DateTimePicker").date());
                var s = m.format('DD/MM/YYYY HH:mm');
                
                $("#<%= txtquizAvtiveDateTime.ClientID%>").val(s);
               
            });

   
        });

        </script>

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h2>
                    <asp:Label ID="lblQuizHeading" runat="server">Create New Quiz</asp:Label>
                </h2>
                <p class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </p>
            </div>
        </div>

        <br />
        <div class="row">

            <div class="form-horizontal">

                <div class="col-md-6">
                    <fieldset>

                        <legend>Details</legend>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtquizTitle" CssClass="col-md-3 control-label">Title</asp:Label>
                            <div class="col-md-9">
                                <asp:TextBox runat="server" ID="txtquizTitle" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtquizTitle"
                                    CssClass="text-danger" ErrorMessage="The Title field is required." Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtquizDescription" CssClass="col-md-3 control-label">Description</asp:Label>
                            <div class="col-md-9">
                                <asp:TextBox runat="server" ID="txtquizDescription" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtquizDescription"
                                    CssClass="text-danger" ErrorMessage="The Description field is required." Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-3 control-label"><Strong>Activation Date</Strong></asp:Label>
                            <div class="col-md-9 OverrideBootStrapLeftPadding">
                                <div class='input-group date  col-md-12' id='datetimepicker2'>
                                    <asp:HiddenField runat="server" ID="txtquizAvtiveDateTime" />
                                </div>
                            </div>
                        </div>


                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtquizPrizeMoney" CssClass="col-md-3 control-label">Prize Money</asp:Label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtquizPrizeMoney" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtquizPrizeMoney"
                                    CssClass="text-danger" ErrorMessage="The Prize money field is required." Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtquizTimesNumberOfEntriesAllowed" CssClass="col-md-3 control-label">Total Entries</asp:Label>
                            <div class="col-md-5 ">
                                <asp:TextBox runat="server" ID="txtquizTimesNumberOfEntriesAllowed" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtquizTimesNumberOfEntriesAllowed"
                                    CssClass="text-danger" ErrorMessage="The Total entries field is required." Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtquizWinnerNumber" CssClass="col-md-3 control-label">Winner Number</asp:Label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtquizWinnerNumber" CssClass="form-control" />
                                <p>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtquizWinnerNumber"
                                        CssClass="text-danger" ErrorMessage="The winner number field is required." Display="Dynamic" />
                                </p>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtquizWinnerNumber"
                                    Operator="LessThanEqual" ControlToCompare="txtquizTimesNumberOfEntriesAllowed" CssClass="text-danger"
                                    ErrorMessage="Please select winner number less than total entries." Type="Integer" Display="Dynamic" />
                            </div>
                        </div>

                    </fieldset>
                </div>

                <!-- question section -->
                <div class="col-md-6">
                    <fieldset>
                        <legend>Question</legend>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtquizTitle" CssClass="col-md-4 control-label">Text</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtquizQuestionText" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtquizQuestionText"
                                    CssClass="text-danger" ErrorMessage="The question text field is required." Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtquizTitle" CssClass="col-md-4 control-label">Upload</asp:Label>
                            <div class="col-md-8">
                                <label class="checkbox-inline">
                                    <input type="checkbox" id="cbImageUpload" value="1" class="chb" runat="server" />Image                                    
                                </label>
                                <label class="checkbox-inline">
                                    <input type="checkbox" id="cbVideoUpload" value="2" class="chb" runat="server" />Video  
                                    <a href="#">
                                        <span class="glyphicon glyphicon-question-sign"></span>
                                    </a>
                                </label>

                                <p class="help-block">Upload picture or video <u>only</u> if quiz question needs it.</p>
                            </div>
                        </div>

                        

                        <div id="divUploadFile" class="form-group hidden" style="margin-top: -10px;">
                            <div class="col-md-offset-4 col-md-8">
                                <a class='btn btn-primary' href='javascript:;'>Choose File...
			                        <input type="file" class="btn-file" name="uploadFileName" size="40"
                                        onchange='$("#upload-file-info").html($(this).val());' />
                                </a>
                                <br />
                                <span class='label label-info' id="upload-file-info"></span>
                                <br />
                               
                            </div>
                        </div>

                       

                        <div class="form-group" style="display: none;">
                            <asp:Label runat="server" AssociatedControlID="txtquizQuestionNumberOfAnswers" CssClass="col-md-4 control-label">Number Of Answers</asp:Label>

                            <div class="col-md-4">
                                <asp:DropDownList ID="txtquizQuestionNumberOfAnswers" class="form-control input-sm" runat="server" EnableViewState="true" />
                                <asp:CompareValidator ID="cmpCountry" runat="server" ControlToValidate="txtquizQuestionNumberOfAnswers" Operator="NotEqual" ValueToCompare="0" Type="Integer"
                                    ErrorMessage="Please select number of answers." CssClass="text-danger" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtquizAnswerText1" CssClass="col-md-4 control-label">Answer 1</asp:Label>
                            <div class="col-md-8">

                                <asp:TextBox runat="server" ID="txtquizAnswerText1" CssClass="form-control" TextMode="SingleLine" />

                                <input type="checkbox" id="chkCorrectAnswer1" class="chbAns checkbox-inline" runat="server" />
                                <br />

                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtquizAnswerText1"
                                    CssClass="text-danger" ErrorMessage="The answer text field is required." Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtquizAnswerText2" CssClass="col-md-4 control-label">Answer 2</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtquizAnswerText2" CssClass="form-control" TextMode="SingleLine" />
                                <input type="checkbox" id="chkCorrectAnswer2" class="chbAns checkbox-inline" runat="server" />
                                <br />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtquizAnswerText2"
                                    CssClass="text-danger" ErrorMessage="The answer text field is required." Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtquizAnswerText3" CssClass="col-md-4 control-label">Answer 3</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtquizAnswerText3" CssClass="form-control" TextMode="SingleLine" />
                                <input type="checkbox" id="chkCorrectAnswer3" class="chbAns checkbox-inline" runat="server" />
                                <br />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtquizAnswerText3"
                                    CssClass="text-danger" ErrorMessage="The answer text field is required." Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtquizAnswerText4" CssClass="col-md-4 control-label">Answer 4</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtquizAnswerText4" CssClass="form-control" TextMode="SingleLine" />
                                <input type="checkbox" id="chkCorrectAnswer4" class="chbAns checkbox-inline" runat="server" />
                                <br />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtquizAnswerText4"
                                    CssClass="text-danger" ErrorMessage="The answer text field is required." Display="Dynamic" />
                            </div>
                        </div>

                        <br />

                        <div class="form-group">
                            <div class="col-md-offset-10 col-md-2 pull-left">
                                <asp:Button runat="server" OnClick="AddQuiz_Click" Text="Save" CssClass="btn btn-lg btn-default btn-danger" />
                            </div>
                        </div>


                    </fieldset>
                </div>

            </div>



        </div>


    </div>
    <asp:HiddenField ID="quizID" runat="server" />
</asp:Content>
