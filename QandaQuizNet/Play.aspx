<%@ Page Title="Play" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Play.aspx.cs" Inherits="QandaQuizNet.Play" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href='<%= Page.ResolveUrl("/content/flipclock.css") %>'>
    <link href='<%= Page.ResolveUrl("/content/TimeCircles.css") %>' rel="stylesheet">
    <link rel="stylesheet" href='<%= Page.ResolveUrl("/content/magnific-popup.css") %>' >

    <style type="text/css">
        .classWithPad {
            /*margin: 10px;*/
            padding: 10px;
        }


        .NoPadding {
            padding-left: 0px;
            padding-right: 0px;
        }
    </style>
    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="scriptTemplate" runat="server">

    <script src='<%= Page.ResolveUrl("/Scripts/flipclock.js") %>'></script>
    <script type="text/javascript" src='<%= Page.ResolveUrl("/Scripts/TimeCircles.js") %>'></script>
    <script type="text/javascript" src='<%= Page.ResolveUrl("/Scripts/jquery.magnific-popup.js") %>'></script>
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

        $(document).ready(function () {

            $("input[type=radio]").click(function () {
                $("#<%= noAnswerSelected.ClientID %>").removeClass("visible").addClass("hidden");
            });
            $(".countdownTimer").attr("data-timer", "<%--= NextQuizTimeInSecs --%>");
            $(".countdownTimer")
                .TimeCircles({ count_past_zero: false })
                .addListener(function(unit, amount, total){
                    if (total == 0)
                    {
                        alert("This quiz has now expired. \n The page will auto-refresh to show new question. Good luck with the quiz!");
                        window.location.reload(1);
                    }
                });

            $('.test-popup-link').magnificPopup({
                type: 'image'
                // other options
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

    <div class="container-fluid" style="padding: 0px;">

        <div class="col-md-12 answerFeedback" runat="server" id="CorrectAnswer" visible="false">
            <div class="alert alert-success fade in">
                <a href="#" class="close  fa-close icon" data-dismiss="alert" aria-label="close"></a>
                <p>
                    <i class="correctAnswer icon fa-check-square-o fa-2x"></i>
                    <strong class="correctAnswer">Correct!</strong>
                    <br />
                    Thanks for entering. Find out if you answered correctly on our facebook page after 9pm. <br/>
                    Why not play again and increase your odds of winning the quiz.
                </p>
            </div>
        </div>

        <div class="col-md-12 answerFeedback" runat="server" id="FreeCorrectAnswer" visible="false">
            <div class="alert alert-warning fade in">
                <a href="#" class="close  fa-close icon" data-dismiss="alert" aria-label="close"></a>
                <p>
                    <i class="correctAnswer icon fa-check-square-o fa-2x"></i>
                    <strong class="correctAnswer">Correct!</strong>
                    <br />
                    Play to enter the draw to win the prize.
                </p>
            </div>
        </div>

        <div class="col-md-12 answerFeedback" runat="server" id="WrongAnswer" visible="false">
            <div class="alert alert-danger fade in">
                <a href="#" class="close fa-close icon" data-dismiss="alert" aria-label="close"></a>
                <p>
                    <i class="IncorrectAnswer icon fa-times-circle-o fa-2x"></i>
                    <strong class="IncorrectAnswer">Incorrect! </strong>
                    <br />
                    What's the polite way of saying you got it wrong, why not play again and choose correct answer.
                </p>
            </div>
        </div>

        <div class="container-fluid" style="margin-top: 2px; padding: 0px;">
            <div class="row">
                <div class="col-sm-6 quizsection NoPadding">

                    <div class="row centered">
                        <div class="PrizeMoneyAnnouncement">
                            <h2 id="currentQuizPrizeMoney" runat="server" class="icon fa-trophy"></h2>
                        </div>
                    </div>

                    <div class="row centered oddsLinkOnPlay">
                        <div class="col-sm-12">
                            <a href="/Info#odds">Click here to see odds of winning Q&A quiz</a>
                        </div>
                    </div>

                    <div class="row rowOne">


                        <div class="row quizQuestion">
                            <div class="col-md-12 col-xs-12">
                                <asp:Label class="form-control-static" runat="server" ID="lblQuestionText" Text="" />
                            </div>
                        </div>
                        <div class="row" runat="server" id="imagePlaceholder" visible="false">
                            <div class="col-md-12">
                                <div class="col-md-6 " style="border: 0px dotted green; margin-top: 10px;">
                                    <a class="test-popup-link" runat="server" id="imageQuestion" href="Uploads/1630_20151010_022609_20151206_014733.jpg">Click here to see image</a>

                                    <%--<img class="img-responsive" runat="server" id="imgQuestion" src=""
                                        style="width: 100%; max-height: 100%; margin: auto; display: block;" />--%>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <hr />
                            </div>
                        </div>

                        <div class="row col-md-12 col-xs-offset-1 col-xs-8" id="donate" style="padding-left: 0px;">
                            <%--<div class=" " >--%>
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="blue">
                                        <input type="radio" id="answerToQuiz_1" name="answerToQuiz" runat="server">
                                        <span id="answerText1" runat="server"></span>
                                    </label>
                                </div>
                                <div class="col-md-6">
                                    <label class="blue">
                                        <input type="radio" id="answerToQuiz_2" name="answerToQuiz" runat="server">
                                        <span id="answerText2" runat="server"></span>
                                    </label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <label class="blue">
                                        <input type="radio" id="answerToQuiz_3" name="answerToQuiz" runat="server">
                                        <span id="answerText3" runat="server"></span>
                                    </label>
                                </div>
                                <div class="col-md-6">
                                    <label class="blue">
                                        <input type="radio" id="answerToQuiz_4" name="answerToQuiz" runat="server">
                                        <span id="answerText4" runat="server"></span>
                                    </label>
                                </div>
                            </div>
                            <%-- </div>--%>
                        </div>

                        <div class="row">
                            <div class="col-md-12 submitButton">
                                Please select correct answer for the <strong>Q&A Quiz</strong> and click on submit.
                            </div>
                        </div>

                        <!--submit button -->
                        <div class="row">
                            <div class="col-md-12 text-left submitButton">

                                <asp:LinkButton ID="LinkButton1" runat="server" Text="SUBMIT" CssClass="btn btn-primary btn-lg customBtn"
                                    OnClientClick="return ValidateQuizAnswer();" OnCommand="Submit_Click" CommandArgument="PlayToWin">
                                    <span class="glyphicon glyphicon-ok"></span>&nbsp; Yes, Play to Win!                                
                                </asp:LinkButton>
                                                              
                            </div>
                          </div>

                          <div class="row">
                             <div class="col-md-12 text-left submitButton">

                                <asp:LinkButton ID="LinkButton2" runat="server" Text="SUBMIT" CssClass="btn btn-danger btn-lg customBtn"
                                    OnClientClick="return ValidateQuizAnswer();" OnCommand="Submit_Click" CommandArgument="PlayForFree">
                                    <span class="glyphicon"></span>&nbsp;Play for free!                                
                                </asp:LinkButton>
                                
                            </div>
                            
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <span id="noAnswerSelected" class="hidden icon fa-warning" runat="server">Please select your answer! </span>                                
                            </div>
                        </div>
                    </div>                    

                    <div class="row rowTwo">
                        <div class="col-sm-12 " style="width: 100%;">
                            <div class="message">Time Left</div>
                            <div class="countdownTimer" data-timer="0" id="countdownTimer1" runat="server"></div>
                        </div>
                    </div>

                </div>

                <div class="col-sm-6 NoPadding">
                    <div class="row  no-gutter hidden-xs quizLogoOpen row-eq-height">
                        <%--<div class="col-sm-12 quizLogoOpen">--%>
                        <div class="col-sm-6">
                            <img id="imgLogo" runat="server" src="~/images/Qanda Logo-Pound.jpg" class="img-responsive img-phone-hand" />
                        </div>
                        <div class="col-sm-6" style="border: 0px solid red;">
                            <div class="row">
                            <span class="competitionOpenText">COMPETITION </span>
                            <br />
                            <span class="blink_me competitionOpenText openBlinkText">OPEN</span>
                            </div>
                            <div class="row OddsOfWinningText">
                                Odds to win this quiz are 1 in <span id="numOfEntriedAllowedForQuiz" runat="server">5000</span> or better.
                            </div>                 
                        </div>
                        <%--</div>--%>
                    </div>

                    <div class="row  no-gutter">
                        <div class="col-sm-5 quizComingSoon NoPadding">
                            <div style="text-align: center;">
                                <h3><strong>Coming Soon</strong></h3>
                            </div>

                            <p class="marqueeDownUp">
                                <span runat="server" id="TenQuizScrollList" style="width: 100%;"></span>
                            </p>
                        </div>

                        <div class="col-sm-7 NoPadding">
                            <div class="row  no-gutter">
                                <div class="col-sm-12 centered followUsSection" style="width: 100%;">

                                    <h3><strong>Follow us on social media</strong></h3>
                                    <a href="https://www.facebook.com/QandAQuiz/" target="_blank" title="follow us on Facebook" class="icon fa-facebook fa-2x FollowfacebookIcon"></a>
                                    <a href="https://twitter.com/qandaquiz" target="_blank" title="follow us on Twitter" class="icon fa-twitter fa-2x FollowtwitterIcon"></a>
                                    <a href="https://medium.com/@QandAQuiz" target="_blank" title="follow us on Medium" class="icon fa-medium fa-2x"></a>
                                </div>

                                <div class="col-sm-12 quizPastWinner">
                                    <div style="text-align: center;">
                                        <h3><strong>Past Winners</strong></h3>
                                    </div>
                                    <div class="classWithPad">

                                        <p class="marqueeUpDown" style="margin-left: 0px;">
                                            <span id="TenWinnerScrollList" runat="server">
                                                Vipin, Exeter<br />
                                                <br />
                                                Richard, Isle of Man<br />
                                                <br />
                                                Anthony, Ottawa<br />
                                                <br />
                                                Shauna, Suffolk<br />
                                                <br />
                                                John, East London<br />
                                                <br />
                                                Michael, Montreal<br />
                                                <br />
                                                Cyan, Toronto<br />
                                                <br />
                                                Suzzane, Edinburgh<br />
                                                <br />
                                                Steve, Manchester<br />
                                                <br />
                                                Leo, Quebec City<br />
                                                <br />
                                            </span>
                                        </p>


                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </div>
</asp:Content>
