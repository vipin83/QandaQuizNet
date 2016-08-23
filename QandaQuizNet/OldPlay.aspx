<%@ Page Title="Play" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OldPlay.aspx.cs" Inherits="QandaQuizNet.OldPlay" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href='<%= Page.ResolveUrl("/content/flipclock.css") %>'>
    
    <style type="text/css">

        @media only screen and (max-width: 768px) {
            .clock {
                zoom: 0.6;
                -moz-transform: scale(0.6);
                -webkit-transform: scale(0.6);
                align-content: center;
            }

            #quizSection {
                margin-top:10px;
            }
            
            #pastWinners
            {
                margin-top:10px;
            }
        }

         @media only screen and (min-width: 768px) {
            .clock {
                zoom: 1;
                -moz-transform: scale(1);
                -webkit-transform: scale(1);
                align-content: center;
            }

            /*.alignClockCenter{
                width:70%;
                margin:20px auto;
            }*/
        }
          
         .CompetitionOpenLogo
         {
             padding:20px 0;
             margin-bottom:20px;
         }

         .correctAnswer
         {
             color:green;
             font-size:1.25em;
         }

         .IncorrectAnswer
         {
             color:red;
             font-size:1.25em;
         }

        .classWithPad {
            /*margin: 10px;*/
            padding: 10px;
        }

        /* remove spacing between middle columns */
        .row .no-gutter [class*='col-']:not(:first-child):not(:last-child) {
            padding-right: 5px;
            padding-left: 5px;
        }
        /* remove right padding from first column */
        .row .no-gutter [class*='col-']:first-child {
            padding-right: 5px;
        }
        /* remove left padding from first column */
        .row .no-gutter [class*='col-']:last-child {
            padding-left: 5px;
        }

        .row-eq-height {
            display: -webkit-box;
            display: -webkit-flex;
            display: -ms-flexbox;
            display: flex;
        }


        .row {
            margin-left: 0px;
            margin-right: 0px;
        }


  </style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="scriptTemplate" runat="server"> 
    
     <script src='<%= Page.ResolveUrl("/Scripts/flipclock.js") %>'></script>
     <script type="text/javascript">    
         
         function ValidateQuizAnswer() {
             if ($("input[type=radio]:checked").length <= 0) {
                 $("#<%= noAnswerSelected.ClientID %>").removeClass("hidden").addClass("visible");
                 return false;
             }
             else {
                 $("#<%= noAnswerSelected.ClientID %>").removeClass("visible").addClass("hidden");
                 return true;
             }

         }

        var clock;
        $(document).ready(function() {
			
            // Grab the current date
            var currentDate = new Date();

            // Set date for the next quiz active date time
            var futureDate = new Date("<%= NextQuizActiveDateTime.ToString("yyyy-MM-ddTHH:mm")%>"); //new Date(currentDate.getFullYear(), 1, 8);

            // Calculate the difference in seconds between the future and current date
            var diff = futureDate.getTime() / 1000 - currentDate.getTime() / 1000;

            // Instantiate a coutdown FlipClock
            clock = $('.clock').FlipClock(diff, {
                clockFace: 'DailyCounter',
                countdown: true,
                showSeconds:true
            });

            $("input[type=radio]").click(function () {
                $("#<%= noAnswerSelected.ClientID %>").removeClass("visible").addClass("hidden");
            });

        });

         function blinker() {
             $('.blink_me').fadeOut(500);
             $('.blink_me').fadeIn(500);
         }

         setInterval(blinker, 1000);
    </script>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-md-12" runat="server" id="CorrectAnswer" visible="false">
            <div class="alert alert-info fade in">
                <a href="#" class="close" data-dismiss="alert" aria-label="close"><strong>&times;</strong></a>
                <p>
                    <strong class="correctAnswer">
                        You answered it correctly. 
                   </strong>
                    <br />
                    Why not play again and increase your odds of winning the quiz.
                </p>
            </div>
    </div>

    <div class="col-md-12" runat="server" id="WrongAnswer" visible="false">
            <div class="alert alert-info fade in">
                <a href="#" class="close" data-dismiss="alert" aria-label="close"><strong>&times;</strong></a>
                <p>
                    <strong class="IncorrectAnswer">
                        You answered it wrong! 
                   </strong>
                    <br />
                    We understand you may have pressed a wrong button, why not play again and choose correct answer.
                </p>
            </div>
    </div>

    <div class="row" style="margin-top:15px;">
        <div class="col-md-4">
            <img id="imgLogo" runat="server" src="~/images/Qanda Logo-Pound.jpg" class="img-responsive img-phone-hand" />
        </div>
        <div class="col-md-offset-2 col-md-4 blink_me">
            <h1 id="currentQuizPrizeMoney" runat="server"></h1>
        </div>
    </div>

    <div class="row col-lg-push-12 hidden-sm hidden-xs" >
        <div class="CompetitionOpenLogo">
            <img class="img-responsive" src='<%= Page.ResolveUrl("/images/Competition-Open.gif") %>' style="width:100%;height:100%;" />
        </div>
    </div>

    <div class="row no-gutter">

        <!--quiz section -->
        <div class="col-sm-7 col-sm-push-2" id="quizSection" style="padding-left: 5px; padding-right: 5px; margin-left:0px; border:0px solid green;">
            <div class="row" style="border: 3px solid gold;">
                <div class="classWithPad">
                    <!--Question row -->
                    <div class="row quizQuestion">                  
                        <div class="col-md-12 col-xs-12">
                            <asp:Label class="form-control-static" runat="server" ID="lblQuestionText" />
                        </div>
                    </div>

                    <!--placeholder for image or video (TODO) -->
                    <div class="row" runat="server" id="imgVideoPlaceholder" visible="false">
                        <div class="col-md-12">
                            <div class="col-md-6 "
                                style="border: 0px dotted green; margin-top: 10px;">

                                <img class="img-responsive" runat="server" id="imgQuestion" src=""
                                    style="width: 100%; max-height: 100%; margin: auto; display: block;" />
                            </div>
                        </div>
                    </div>

                    <!--horizontal line -->
                    <div class="row">
                        <div class="col-md-12">
                            <hr />
                        </div>
                    </div>

                    

                    <!--answer opntions-->
                    <%--<div class="row">
                        <div class="col-md-offset-1 col-md-8 col-xs-offset-1 col-xs-8 radio controls">
                            <asp:RadioButtonList runat="server" ID="answerList" CssClass="rblQuizAnswer"/>
                        </div>
                    </div>--%>

                    <div class="row col-md-offset-1 col-md-8 col-xs-offset-1 col-xs-8" id="donate" style="padding-left:0px;">
                        <%--<div class=" " >--%>
                            <div class="row">
                                <div class="col-md-4" >
                                    <label class="blue">
                                        <input type="radio" id="answerToQuiz_1" name="answerToQuiz" runat="server"> 
                                            <span id="answerText1" runat="server"></span> 
                                    </label>
                                </div>
                                <div class="col-md-4 col-md-offset-1">
                                    <label class="blue">
                                        <input type="radio" id="answerToQuiz_2" name="answerToQuiz" runat="server"> 
                                            <span id="answerText2" runat="server"></span> 
                                    </label>
                                </div>
                            </div>
                        
                            <div class="row">
                                <div class="col-md-4">
                                    <label class="blue">
                                        <input type="radio" id="answerToQuiz_3" name="answerToQuiz" runat="server"> 
                                            <span id="answerText3" runat="server"></span> 
                                    </label>
                                </div>
                                <div class="col-md-4 col-md-offset-1">
                                    <label class="blue">
                                        <input type="radio" id="answerToQuiz_4" name="answerToQuiz" runat="server"> 
                                            <span id="answerText4" runat="server"></span> 
                                    </label>
                                </div>
                            </div>
                       <%-- </div>--%>
                    </div>

                    <div class="row">
                        <div class="col-md-12" style="margin-top:10px;">
                            Please select correct answer for the <strong>Q&A Quiz</strong> and click on submit.
                        </div>
                    </div>

                    <!--submit button -->
                    <div class="row">
                        <div class="col-md-12 text-left"  style="margin-top:10px;">
                        
                            <asp:LinkButton ID="LinkButton1" runat="server" Text="SUBMIT" CssClass="btn btn-danger btn-lg customBtn" 
                                OnClientClick="return ValidateQuizAnswer();" OnClick="Submit_Click">
                                <span class="glyphicon glyphicon-ok"></span>&nbsp;Submit                                
                            </asp:LinkButton>
                            <br />

                            <span id="noAnswerSelected" class="hidden" runat="server">
                                <span class="glyphicon glyphicon-alert"></span>
                                Please select your answer!
                            </span>
                        </div>
                    </div>

                </div>
            </div>
            
            <div class="row" style="margin-top:10px;">
                <div class="message" style="font-size:20px;text-align:center; color:red;font-weight:bold;">Time left </div>
                <div class="alignClockCenter">
                    <div class="clock"></div>
                </div>    
                
            </div>
        </div>

        <!--upcoming quiz scroll section -->
        <div class="col-sm-2 col-sm-pull-7" id="comingSoon" style="padding-left: 0; padding-right: 0;">
            
            <div class=" row col-md-12 bg-danger" style="border: 3px solid red; height: 300px;">
                <div style="text-align:center;"><h4><strong>Coming Soon</strong></h4></div>
                <p class="marqueeDownUp">
                       <span id="TenQuizScrollList" runat="server">
                       </span>
                   </p>
                </div>

           <%-- <!-- quiz scrolling text -->
            <div class="row col-md-12 bg-danger" style="border: 3px solid red; height: 250px;">
                
            </div>--%>

        </div>

        <!--past winnder scroll section -->
        <div class="col-sm-3 bg-info" id="pastWinners" style="border: 3px solid blue; height: 300px;">
            <div style="text-align:center;"><h4><strong>Past Winners</strong></h4> </div>
            <div class="classWithPad">
               
                 <p class="marqueeUpDown">
                       <span id="TenWinnerScrollList" runat="server">
                            Vipin A, Exeter<br />
                            Richard C, Isle of Man<br />
                            Anthony Smith, Ottawa<br />
                            Shauna McKinskey, Suffolk<br />
                            John Guy, East London<br />
                            Michael Blackwood, Montreal <br />
                            Cyan Presley, Toronto<br />
                            Suzzane Carpenter, Edinburgh<br />
                            Steve Cooper, Manchester<br />
                            Leo Pitt, Quebec City<br />
                       </span>
                   </p>
                   
               
            </div>
        </div>

    </div>

</asp:Content>
