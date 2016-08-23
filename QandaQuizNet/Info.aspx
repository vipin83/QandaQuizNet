<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Info.aspx.cs"
    Inherits="QandaQuizNet.Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <%-- <div id="herowrap" name="hero">
        <div id="welcome" class="container">


            <div class="row">
                <div class="col-md-5">
                    <div class="phone-hand">
                        
                        <img id="imgLogo" runat="server" src="~/images/Qanda Logo-Pound.jpg" class="img-responsive img-phone-hand" />
                    </div>
                </div>
                <div class="col-md-7">
                    <div class="hero-right-text">
                        <div class="blue-bg">
                            <h1>Welcome to Q&A Quiz</h1>
                        </div>

                        <div class="blue-bg row" style="margin-top: 20px; padding: 5px">
                            <h2>Real money, of real use, to real people</h2>
                        </div>

                        <div class="hero-right-text-transparent-black" style="margin-top: 20px; padding-top: 12px">Have fun,</div>
                        <div class="hero-right-text-transparent-black">impress your friends and have</div>
                        <div class="hero-right-text-transparent-black" style="padding-bottom: 12px;">a really good shot at winning money.</div>
                       
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <!-- /headerwrap -->

    <div class="row" id="how-it-works">
        <br />
    </div>

    <div class="top-row">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h2 class="about-us">What would you do with your cash prize?</h2>
                </div>
            </div>

            <div class="row col-lg-push-12 hidden-sm hidden-xs">
                <div class="CompetitionOpenLogo">
                    <img class="img-responsive" src='<%= Page.ResolveUrl("/images/WhatWillYouBuyWithPrizeMoney.jpg") %>' style="width: 100%; height: 100%;" />
                </div>
            </div>


        </div>
    </div>

    <div class="white-row">
        <div class="container">
            <div class="row">
                <div class="col-md-10 col-md-offset-2">
                    <div class="every-qanda-center">
                        Every <strong>Q&A</strong> quiz will be won<sup style="font-size:0.5em;">*</sup> and  every winner will receive the cash prize!
					    <br />                        
                        <span style="font-size:0.5em;">* subject to minimum number of entries</span>
                    </div>
                    
                </div>
            </div>
        </div>
    </div>

    <div class="blue-row">
        <div class="container">

            <div class="row">
                <div class="col-sm-2 ">
                    <img runat="server" src="~/images/smiley-1.png" class="img-responsive img-center-align-mobile" />
                </div>
                <div class="col-sm-10 text-center-align-mobile">

                    <strong>Q&A</strong> only takes a minute to play and can be played anywhere, anytime. Once a <strong>Q&A</strong> quiz is won,  a new competition is posted. The questions are always different and cover a wide range of topics to keep you interested &amp; challenged.
					
				
                </div>
            </div>

            <div class="row" style="margin-top: 30px; text-align: right;">
                <div class="col-sm-2 col-sm-push-10 center-align-mobile">
                    <img runat="server" src="~/images/smiley-2.png" class="img-responsive img-center-align-mobile" />
                </div>
                <div class="col-sm-10 col-sm-pull-2 text-center-align-mobile" style="text-align: left;">
                    Initially, <strong>Q&A</strong> quiz prizes will be &pound;1,000 or less. You can help steer the direction of <strong>Q&A</strong>. Your input will affect the prize amounts, the <strong>Q&A</strong> quiz topics and the complexity of the questions.
				
                </div>
            </div>

            <div class="row" style="margin-top: 30px;">
                <div class="col-sm-2 center-align-mobile">
                    <img runat="server" src="~/images/smiley-3.png" class="img-responsive img-center-align-mobile" />
                </div>
                <div class="col-sm-10 text-center-align-mobile">
                    So sit back, lie down, stand up or kneel down and get ready to have some fun. Q&A only costs &pound;1 to play for the cash prize. Rules are fair and simple. Q&A the QUIZ of Choice<sup>&trade;</sup><br />
                    <strong>…MAKE IT YOUR GAME</strong>.  
				
                </div>
            </div>

        </div>
    </div>

    <div class="white-row">
        <div class="container">

            <div class="row">
                <div class="col-md-12">
                    <h2 class="how-qanda-works">How Q&A works</h2>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3 col-md-2 col-md-offset-2">
                    <img runat="server" src="~/images/qandaquiz-iPhone.png" class="img-responsive img-iphone" />
                </div>
                <div class="col-sm-9 col-md-6 text-center-align-mobile">
                    <strong>Q&A</strong> <span class="font-light">is a question and answer game, a quiz wherein each question focuses on a specific topic including Sports, News, Current Events, Music, TV Soaps, World Flags, Celebrities, Logos, Entertainment, News etc.  Each game has a cash prize value up to a current maximum of £1000; each game costs £1 to enter.  Basically, three steps are required: [1] register [2] deposit [3] play.  You can register with us or use your Facebook login.  Deposit (minimum of £5) is made via PayPal and credits are then listed in your account profile (each credit valued at £1). Each game has a minimum of 1 question covering various topics; each question has 4 multiple choice answers to choose from.  Answer the question in the competition and enter to win (cost of 1 credit valued at £1).  As soon as the game is won, the winner is announced and a new competition with a new question or set of questions starts.</span>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <h3 class="example">EXAMPLE</h3>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3 col-sm-push-9 center-align-mobile">
                    <img runat="server" src="~/images/qandaquiz-joypad.png" class="img-responsive img-center-align-mobile" />
                </div>
                <div class="col-sm-9 col-sm-pull-3 example-text text-center-align-mobile" style="text-align: left;">
                    You register and deposit funds via PayPal; you receive a confirmation text or email verifying the transaction and listing the credits in your account. You click on the Active Competition link and play the game; games will be removed and replaced as soon as there is a winner. If you win, your winnings will be transferred into your PayPal account or <strong>Q&A</strong> credit account depending on your choice when registering.Each game bears a name or ID number and will be shown along with the winner and prize amount in the Winner’s Archive.Terms and Conditions and Privacy Policy are listed here for your convenience.
				
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-md-8 col-md-offset-2">
                    <img runat="server" src="~/images/qandaquiz-pictogram.png" style="margin: 60px auto 0 auto;" class="img-responsive" />
                </div>
            </div>
        </div>
    </div>

    <div id="about-us" class="white-row">
        <div class="container">
            <div class="row">
                <div class="col-xs-10 col-xs-offset-1 col-md-6 col-md-offset-3">
                    <hr />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h2 class="about-us">About us</h2>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2 col-sm-offset-1 center-align-mobile">
                    <img runat="server" src="~/images/qandaquiz-about-us.png" class="img-responsive img-center-align-mobile" />
                </div>
                <div class="col-sm-8 about-us-text text-center-align-mobile">
                    We are a British Isles company dedicated to operating a fun and transparent game that offers people like you the opportunity to compete against others in solving interesting questions across numerous subjects. Our business model is built on a foundation of integrity, fairness and value; we believe passionately in what we do and are committed to providing you with enjoyment, entertainment and a little enlightenment. Our corporate philosophy of ethical and honest practices extends to our suppliers, employees and especially you, our customers. Additionally, we embrace our altruistic side and actively contribute  to bona fide charities located in the British Isles.
				
                </div>
            </div>
        </div>
    </div>

    <div id="odds" class="white-row">
        <div class="container">
            <div class="row">
                <div class="col-xs-10 col-xs-offset-1 col-md-6 col-md-offset-3">
                    <hr />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h2 class="about-us">What are the chances?</h2>
                </div>
            </div>

            <div class="row">
                <div class="col-md-offset-2 col-md-10">
                    <table class="table table-bordered table-condensed">
                        <thead>
                            <tr>
                                <th>BEST VALUE</th>
                                <th>ODDS</th>
                                <th>PRIZE</th>
                                <th>COST</th>
                                <th>PROCESS</th>
                                <th>WINNER</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class="success">
                                <td>Q&A Quiz</td>
                                <td>1 IN 2,500</td>
                                <td>£1,000</td>
                                <td>£1</td>
                                <td>FORMULA</td>
                                <td>GUARANTEED</td>
                            </tr>
                             <tr class="ACTIVE">
                                <td>LUCKY LINES</td>
                                <td>1 IN 11,957</td>
                                <td>£40</td>
                                <td>£1</td>
                                <td>RANDOM</td>
                                <td>MAYBE</td>
                            </tr>
                            <tr class="ACTIVE">
                                <td>PIGS MIGHT FLY</td>
                                <td>1 IN 22,827</td>
                                <td>£100</td>
                                <td>£1</td>
                                <td>RANDOM</td>
                                <td>MAYBE</td>
                            </tr>
                            <tr class="ACTIVE">
                                <td>BEE LUCKY</td>
                                <td>1 IN 122,936</td>
                                <td>£200</td>
                                <td>£1</td>
                                <td>RANDOM</td>
                                <td>MAYBE</td>
                            </tr>
                            <tr class="ACTIVE">
                                <td>100,000 RED</td>
                                <td>1 IN 1,330,181</td>
                                <td>£1,000</td>
                                <td>£1</td>
                                <td>RANDOM</td>
                                <td>MAYBE</td>
                            </tr>
                             <tr class="ACTIVE">
                                <td>TV PHONE TEXT</td>
                                <td>1 IN 100,000</td>
                                <td>£6,000</td>
                                <td>£3</td>
                                <td>RANDOM</td>
                                <td>YES</td>
                            </tr>
                             <tr class="ACTIVE">
                                <td>HEALTH LOTTERY</td>
                                <td>1 IN 2,118,760</td>
                                <td>£100,000</td>
                                <td>£1</td>
                                <td>RANDOM</td>
                                <td>MAYBE</td>
                            </tr>
                             <tr class="ACTIVE">
                                <td>UK LOTTERY</td>
                                <td>1 IN 45,000,000</td>
                                <td>£2,500,000</td>
                                <td>£2</td>
                                <td>RANDOM</td>
                                <td>MAYBE</td>
                            </tr>
                             <tr class="ACTIVE">
                                <td>EUROMILLIONS</td>
                                <td>1 IN 116,531,800</td>
                                <td>£10,000,000</td>
                                <td>£2</td>
                                <td>RANDOM</td>
                                <td>MAYBE</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-1 col-sm-offset-2 center-align-mobile">
                    <img runat="server" src="~/images/qandaquiz-chances-1.png" class="img-responsive img-center-align-mobile" />
                </div>
                <div class="col-sm-7 about-us-text text-center-align-mobile">
                    The modern meaning of probability is a measure of the weight of  empirical evidence, and is arrived at from  inductive reasoning and  statistical inference.  Probability is a measure or estimation of the likelihood of occurrence of an event. Probabilities are given a value between 0 (0% chance or will not happen) and 1 (100% chance or will happen).  
				
                </div>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-sm-1 col-sm-push-9 center-align-mobile">
                    <img runat="server" src="~/images/qandaquiz-chances-2.png" class="img-responsive img-center-align-mobile" />
                </div>
                <div class="col-sm-7 col-sm-pull-1 col-sm-offset-2 about-us-text text-center-align-mobile" style="text-align: left;">
                    In the case of Q&A Cash Quiz, the probability of  a winner for each game is 100%.  Statistically, if  2500 players each paid £1 to participate in a Q&A Quiz for a £1,000 prize, then the odds of any one player winning is 1:2500; however if each player participated multiple times and paid a total of £5, then the player odds may improve.  
				
                </div>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-sm-1 col-sm-offset-2 center-align-mobile">
                    <img runat="server" src="~/images/qandaquiz-chances-3.png" class="img-responsive img-center-align-mobile" />
                </div>
                <div class="col-sm-7 about-us-text text-center-align-mobile">
                    Players must first answer general knowledge skill-testing questions from various categories by choosing the correct response from multiple choice answers.  Players answering the question are then entered into the draw after having paid their £1 entry fee per submission; the winner is selected from the pool of correct entrants using a mathematical formula and a new competition is posted.  Winners are notified and paid; their player identity along with the quiz details are posted in our quiz archives on our website.
				
                </div>
            </div>
        </div>
    </div>

    <div id="top-ten" class="white-row">
        <div class="container">
            <div class="row">
                <div class="col-xs-10 col-xs-offset-1 col-md-6 col-md-offset-3">
                    <hr />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h2 class="about-us"><strong>Q&A</strong> Top Ten: essential highlights of T&amp;C’s</h2>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8 col-sm-offset-2 col-xs-12 font-light">
                    <ul class="qanda-check-list">
                        <li>1. You must be at least 16 years of age to register and play</li>
                        <li>2. You must provide us with honest and accurate information</li>
                        <li>3. You must play honestly, if you are found cheating or manipulating the outcome of a competition, you will be excluded from receiving any prize won, your credits will be forfeited and your account will be terminated</li>
                        <li>4. You can only hold one account at a time</li>
                        <li>5. You are responsible for any activity on your account (so guard your password)</li>
                        <li>6. You will submit your prize receipt form to confirm payment of your prize to your account</li>
                        <li>7. It is understood that circumstances beyond our control may require minor substitution in prizes i.e. colour, style, size, venue etc but in all cases will be of equal or greater value (this is in the case of non-cash prizes)</li>
                        <li>8. We do not employ shill players, fake accounts or false entries to artificially influence our competitions</li>
                        <li>9. All sales are final and non-refundable</li>
                        <li>10. 3rd party mediator will be used to arbitrate any issues not readily reconciled between the player and Ten Penny Ventures PCC.</li>
                    </ul>
                </div>
            </div>

        </div>
    </div>



</asp:Content>
