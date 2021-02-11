var questionCounter = 1;
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