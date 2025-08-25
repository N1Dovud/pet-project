"use strict";
document.addEventListener("DOMContentLoaded", function () {
    const searchTypeSelect = document.getElementById("search-type");
    const queryInput = document.getElementById("query-value");
    const submitBtn = document.getElementById("submit-btn");

    if (searchTypeSelect && queryInput && submitBtn) {
        function updateInputType() {
            const selectedValue = searchTypeSelect.value;
            if (selectedValue === "DueDate" || selectedValue === "CreationDate") {
                queryInput.type = "date";
                submitBtn.style.display = "inline-block";
            }
            else {
                queryInput.type = "text";
                submitBtn.style.display = "none";
            }
        }

        updateInputType();
        searchTypeSelect.addEventListener("change", updateInputType);
    }


    document.addEventListener("click", function (e) {
        const container = e.target.closest(".add-tag-container");
        if (!container) return;
        console.log(e.target);
        const taskId = container.dataset.taskId;
        const returnUrl = container.dataset.returnUrl;

        if (e.target.classList.contains("add-tag-placeholder")) {

            container.innerHTML = `
                <form method="post" action="add-tag" class="d-flex gap-1">
                    <input type="text" id="new-tag-input" name="tagName" class="form-control form-control-sm" placeholder="Enter tag" required />
                    <input type="hidden" name="returnUrl" value="${returnUrl}" />
                    <input type="hidden" name="taskId" value="${taskId}" />
                    <button type="submit" class="save-tag-btn btn btn-success btn-sm">Add</button>
                    <button type="button" class="cancel-tag-btn btn btn-outline-secondary btn-sm">✖</button>
                </form>
                `;
        } else if (e.target.classList.contains("cancel-tag-btn")) {
            e.preventDefault();
            resetUI(container);
        }
    });
    function resetUI(container) {
        container.innerHTML = `<div class="add-tag-placeholder add-tag btn btn-outline-secondary btn-sm d-flex align-items-center">
            <span class="me-1">➕</span> Add Tag
        </div>`;
    }

    document.addEventListener("click", function (e) {
        if (e.target.classList.contains("edit-comment-btn")) {
            const commentId = e.target.dataset.commentId;
            const returnUrl = e.target.dataset.returnUrl;
            const taskId = e.target.dataset.taskId;
            const commentItem = e.target.closest('.comment-item');
            const commentText = commentItem.querySelector('.comment-text');
            const commentActions = commentItem.querySelector('.comment-actions');

            // Store original text BEFORE any manipulation
            const originalText = commentText.textContent.trim();

            // Replace the entire <p> with the form
            commentText.outerHTML = `
            <form method="post" action="edit-comment" class="edit-comment-form">
                <div class="mb-2">
                    <textarea name="note" class="form-control" required rows="3">${originalText}</textarea>
                </div>
                <input type="hidden" name="commentId" value="${commentId}" />
                <input type="hidden" name="returnUrl" value="${returnUrl}" />
                <input type="hidden" name="taskId" value="${taskId}" />
                <div class="edit-actions d-flex gap-2">
                    <button type="submit" class="btn btn-primary btn-sm">Save</button>
                    <button type="button" class="cancel-edit-btn btn btn-outline-secondary btn-sm">Cancel</button>
                </div>
             </form>
        `;

            // Hide comment actions
            commentActions.style.display = 'none';

            // Focus on textarea (need to find it again since DOM changed)
            const form = commentItem.querySelector('.edit-comment-form');
            const textarea = form.querySelector('textarea');
            textarea.focus();
            textarea.select();

            // Handle cancel
            form.querySelector('.cancel-edit-btn').addEventListener('click', function () {
                // Replace form back with <p> tag
                form.outerHTML = `<p class="comment-text card-text mb-2">${originalText}</p>`;
                // Show comment actions again
                commentActions.style.display = 'flex';
            });
        }
    });
});