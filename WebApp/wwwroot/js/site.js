"use strict";
document.addEventListener("DOMContentLoaded", function () {
    document.addEventListener("click", function (e) {
        const container = e.target.closest(".add-tag-container");
        if (!container) return;

        const taskId = container.dataset.taskId;
        const returnUrl = container.dataset.returnUrl;

        if (e.target.classList.contains("add-tag-placeholder")) {

            container.innerHTML = `
                <form method="post" action="add-tag">
                    <input type="text" id="new-tag-input" name="tagName" placeholder="Enter tag" required />
                    <input type="hidden" name="returnUrl" value="${returnUrl}" />
                    <input type="hidden" name="taskId" value="${taskId}" />
                    <button type="submit" class="save-tag-btn">Add</button>
                    <button type="button" class="cancel-tag-btn">✖</button>
                </form>
                `;
        } else if (e.target.classList.contains("cancel-tag-btn")) {
            e.preventDefault();
            resetUI(container);
        }
    });
    function resetUI(container) {
        container.innerHTML = `<div class="add-tag-placeholder add-tag">➕ Add Tag</div>`;
    }
});