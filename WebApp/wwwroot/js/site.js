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

    document.addEventListener("click", function (e) {
        if (e.target.classList.contains("edit-comment-btn")) {
            const commentId = e.target.dataset.commentId;
            const returnUrl = e.target.dataset.returnUrl;
            const commentItem = e.target.closest('.comment-item');
            const commentText = commentItem.querySelector('.comment-text');
            const originalText = commentText.textContent;

            // Replace text with textarea
            commentText.innerHTML = `
                <form method="post" action="edit-comment" class="edit-comment-form">
                    <textarea name="note" required rows="3">${originalText}</textarea>
                    <input type="hidden" name="commentId" value="${commentId}" />
                    <input type="hidden" name="returnUrl" value="${returnUrl}" />
                    <div class="edit-actions">
                        <button type="submit">Save</button>
                        <button type="button" class="cancel-edit-btn">Cancel</button>
                    </div>
                </form>
            `;

            // Hide original edit button
            e.target.style.display = 'none';

            // Focus on textarea
            const textarea = commentText.querySelector('textarea');
            textarea.focus();
            textarea.select();

            // Handle cancel
            commentText.querySelector('.cancel-edit-btn').addEventListener('click', function () {
                commentText.innerHTML = `<p class="comment-text">${originalText}</p>`;
                e.target.style.display = 'inline-block';
            });
        }
    });
});