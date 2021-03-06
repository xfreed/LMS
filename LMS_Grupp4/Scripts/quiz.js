﻿var questionCounter = 1;
var quizQuestions;
var quizAnswer = [];

function addNewQuestion() {

    $("#questionList").append(`<div style="display: inline-block; margin: 10px">
        <label>question text</label><br/>
        <input type="text" id="question_name_${questionCounter}" placeholder="question name"/>
    </div>
    <div style="margin: 10px">
        <input type="text" id="correct_answer_${questionCounter}" value="" placeholder="Correct answer"/>
    </div>
    <div style="margin: 10px">
        <input type="text" id="fake_answer_1_${questionCounter}" value="" placeholder="Fake answer"/>
    </div>
    <div style="margin: 10px">
        <input type="text" id=fake_answer_2_${questionCounter}"" value="" placeholder="Fake answer"/>
    </div>
    <div style="margin: 10px">
        <input type="text" id="fake_answer_3_${questionCounter}" value="" placeholder="Fake answer"/>
    </div>`);
    questionCounter++;
}

function saveNewQuiz() {
    const quizName = $("#quizName").val();
    const htmlQuestionNames = $('[id^="question_name_"]');
    const htmlQuestionAnswers = $('[id^="correct_answer_"]');
    const htmlQuestionFake1 = $('[id^="fake_answer_1_"]');
    const htmlQuestionFake2 = $('[id^="fake_answer_2_"]');
    const htmlQuestionFake3 = $('[id^="fake_answer_3_"]');
    const questionNames = new Array();
    const questionAnswers = new Array();
    const questionFake1 = new Array();
    const questionFake2 = new Array();
    const questionFake3 = new Array();
    for (let i = 0; i < htmlQuestionNames.length; i++) {
        questionNames.push($(htmlQuestionNames[i]).val());
        questionAnswers.push($(htmlQuestionAnswers[i]).val());
        questionFake1.push($(htmlQuestionFake1[i]).val());
        questionFake2.push($(htmlQuestionFake2[i]).val());
        questionFake3.push($(htmlQuestionFake3[i]).val());
    }
    $.ajax({
        type: "POST",
        url: "/Quiz/CreateNewQuiz",
        data: {
            quizName: quizName,
            questionNames: questionNames,
            questionAnswers: questionAnswers,
            questionFake1: questionFake1,
            questionFake2: questionFake2,
            questionFake3: questionFake3
        },
        success(response) {
            if (response) {
                alert("Added new Quiz");
            } else {
                alert("Some trouble...");
            }
        }
    });
}

function startQuiz() {
    const quizId = $("#dropDown-quiz-get").val();
    $.ajax({
        type: "POST",
        url: "/Quiz/Play",
        data: {
            id: quizId
        },
        success(response) {
            if (response.played) {
                $("#played-quiz").show();
            } else {
                questionCounter = 0;
                quizQuestions = response;
                nextQuestion();
                $("#start-quiz").hide();
                $("#played-quiz").hide();
                $("#quiz").show();
            }
        }
    });
}

function nextQuestion() {
    if ($("input[name=question-answers]:checked").val() != null) {
        quizAnswer.push($(`label[for='question-answers-${$("input[name=question-answers]:checked").val()}'`).text());
    } 
    $("#question-text").text(quizQuestions.response[questionCounter].QuestionName);
    const questions = [
        quizQuestions.response[questionCounter].Answer, quizQuestions.response[questionCounter].Fake1,
        quizQuestions.response[questionCounter].Fake2, quizQuestions.response[questionCounter].Fake3
    ];
    shuffle(questions);
    $("label[for='question-answers-A'").text(`A) ${questions[0]}`);
    $("label[for='question-answers-B'").text(`B) ${questions[1]}`);
    $("label[for='question-answers-C'").text(`C) ${questions[2]}`);
    $("label[for='question-answers-D'").text(`D) ${questions[3]}`);
    if (questionCounter + 1 >= quizQuestions.response.length) {
        $("#nextButton").text("Submit!");
        $("#nextButton").attr("onclick", "quizSubmit()");
        return;
    }
    questionCounter++;
}
function shuffle(array) {
    var currentIndex = array.length, temporaryValue, randomIndex;

    // While there remain elements to shuffle...
    while (0 !== currentIndex) {

        // Pick a remaining element...
        randomIndex = Math.floor(Math.random() * currentIndex);
        currentIndex -= 1;

        // And swap it with the current element.
        temporaryValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = temporaryValue;
    }

    return array;
}
function quizSubmit() {
    quizAnswer.push($(`label[for='question-answers-${$("input[name=question-answers]:checked").val()}'`).text());
    $.ajax({
        type: "POST",
        url: "/Quiz/SaveQuizTry",
        data: {
            answerStrings: quizAnswer,
            questions: quizQuestions.response,
            quizId: $("#dropDown-quiz-get").val()
        },
        success(response) {
            $("#quiz").hide();
            $("#scoreSpan").text(`${response.response} of ${questionCounter+1}`);
            $("#finalScore").show();
            questionCounter = 0;

        }
    });
}