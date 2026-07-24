document.addEventListener("DOMContentLoaded", function () {

    const buttons = document.querySelectorAll(".toggle-ai-btn");

    buttons.forEach(btn => {
        btn.addEventListener("click", function () {

            const card = this.closest(".card");
            const explanation = card.querySelector(".ai-explanation");

            if (!explanation) return;

            // Close all other explanations
            document.querySelectorAll(".ai-explanation").forEach(exp => {
                if (exp !== explanation) {
                    exp.classList.add("d-none");
                    exp.closest(".card")
                        .querySelector(".toggle-ai-btn")
                        .innerText = "Why this job?";
                }
            });

            const isHidden = explanation.classList.contains("d-none");

            explanation.classList.toggle("d-none");
            this.innerText = isHidden ? "Hide explanation" : "Why this job?";
        });
    });

});
