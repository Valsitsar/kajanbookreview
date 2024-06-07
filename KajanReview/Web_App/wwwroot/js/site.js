// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*!
 * Color mode toggler for Bootstrap's docs (https://getbootstrap.com/)
 * Copyright 2011-2024 The Bootstrap Authors
 * Licensed under the Creative Commons Attribution 3.0 Unported License.
 */

(() => {
    'use strict'

    const getStoredTheme = () => localStorage.getItem('theme')
    const setStoredTheme = theme => localStorage.setItem('theme', theme)

    const getPreferredTheme = () => {
        const storedTheme = getStoredTheme()
        if (storedTheme) {
            return storedTheme
        }

        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'
    }

    const setTheme = theme => {
        if (theme === 'auto') {
            document.documentElement.setAttribute('data-bs-theme', (window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'))
        } else {
            document.documentElement.setAttribute('data-bs-theme', theme)
        }
    }

    setTheme(getPreferredTheme())

    const showActiveTheme = (theme, focus = false) => {
        const themeSwitcher = document.querySelector('#bd-theme')

        if (!themeSwitcher) {
            return
        }

        const themeSwitcherText = document.querySelector('#bd-theme-text')
        const activeThemeIcon = document.querySelector('.theme-icon-active use')
        const btnToActive = document.querySelector(`[data-bs-theme-value="${theme}"]`)
        const svgOfActiveBtn = btnToActive.querySelector('svg use').getAttribute('href')

        document.querySelectorAll('[data-bs-theme-value]').forEach(element => {
            element.classList.remove('active')
            element.setAttribute('aria-pressed', 'false')
        })

        btnToActive.classList.add('active')
        btnToActive.setAttribute('aria-pressed', 'true')
        activeThemeIcon.setAttribute('href', svgOfActiveBtn)
        const themeSwitcherLabel = `${themeSwitcherText.textContent} (${btnToActive.dataset.bsThemeValue})`
        themeSwitcher.setAttribute('aria-label', themeSwitcherLabel)

        if (focus) {
            themeSwitcher.focus()
        }
    }

    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
        const storedTheme = getStoredTheme()
        if (storedTheme !== 'light' && storedTheme !== 'dark') {
            setTheme(getPreferredTheme())
        }
    })

    window.addEventListener('DOMContentLoaded', () => {
        showActiveTheme(getPreferredTheme())

        document.querySelectorAll('[data-bs-theme-value]')
            .forEach(toggle => {
                toggle.addEventListener('click', () => {
                    const theme = toggle.getAttribute('data-bs-theme-value')
                    setStoredTheme(theme)
                    setTheme(theme)
                    showActiveTheme(theme, true)
                })
            })
    })
})()

function setUserRating(rating) {
    for (let i = 1; i <= 5; i++) {
        let star = document.getElementById(`userRatingStar(${i})`);
        star.classList.remove('bi-star-fill');
        star.classList.add('bi-star');
        if (i <= rating) {
            star.classList.remove('bi-star');
            star.classList.add('bi-star-fill');
        }
    }
}

function playVineBoomSound() {
    let audio = new Audio('~/sounds/vine-boom-sound-effect.mp3');
    audio.play();
}

document.addEventListener("DOMContentLoaded", () => {
    // Get all elements to collapse and the links to show more details
    const descriptionContainers = document.querySelectorAll('.BookItem-description-container');
    const descriptions = document.querySelectorAll('.BookItem-description');
    const moreDetailsLinks = document.querySelectorAll('.BookItem-more-details');

    const checkAndUpdateCollapsible = () => {
        descriptionContainers.forEach((descriptionContainer, index) => {
            const description = descriptions[index];
            const moreDetailsLink = moreDetailsLinks[index];

            if (!descriptionContainer || !description || !moreDetailsLink) {
                console.error('Required elements are missing.');
                return;
            }

            // Get the current heights
            const containerHeight = descriptionContainer.clientHeight;
            const contentHeight = description.scrollHeight;

            console.log(`Container height: ${containerHeight}`);
            console.log(`Content height: ${contentHeight}`);

            // Update classes based on height comparison
            if (contentHeight > containerHeight) {
                descriptionContainer.classList.add('collapsed');
                moreDetailsLink.classList.remove('d-none');
                description.style.display = '-webkit-box';
                console.log('Description is collapsed, and "More details" link is shown.');
            } else {
                descriptionContainer.classList.remove('collapsed');
                moreDetailsLink.classList.add('d-none');
                description.style.display = 'block';
                console.log('Description does not need collapsing.');
            }
        });
    };

    // Initial check with delay to ensure rendering
    setTimeout(checkAndUpdateCollapsible, 100);

    // Check on window resize
    window.addEventListener('resize', checkAndUpdateCollapsible);
    
    // Function to validate change password form
    

    // Add an event listener for the 'submit' event on the change password form
    document.getElementById('changePasswordForm').addEventListener('submit', (event) => {
        // Prevent the default form submission
        event.preventDefault();

        // Create a FormData object from the form
        const formData = new FormData(event.target);

        // Send a POST request to the server with the form data
        fetch('/ProfileEdit?handler=ChangePassword', {
            method: 'POST',
            body: formData
        })
        .then(response => response.json())  // Parse the JSON response from the server
        .then(data => {
            if (data.success) {
                // If the password change was successful, close the modal and refresh the page
                $('#changePasswordModal').modal('hide');
                location.reload();
            } else {
                // If there was an error, show the error message in the modal
                const errorSpan = document.getElementById('changePasswordError');
                errorSpan.textContent = data.error;
                errorSpan.classList.remove('d-none');
            }
        });
    });

    // Clear password fields when the modal is closed
    $('#changePasswordModal').on('hidden.bs.modal', function () {
        document.getElementById('currentPassword').value = '';
        document.getElementById('newPassword').value = '';
        document.getElementById('confirmPassword').value = '';
        document.querySelector('span[asp-validation-for="ChangePasswordError"]').innerText = '';
    });
});

(function () {
    'use strict';
    var forms = document.querySelectorAll('.needs-validation');
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
})();

function displayFileNameAndPreview() {
    const input = document.getElementById('profilePictureInput');
    const fileName = input.files.length > 0 ? input.files[0].name : 'No file selected.';
    document.getElementById('fileName').innerText = fileName;

    // Display image preview with enforced 1:1 aspect ratio
    if (input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = function (e) {
            const img = new Image();
            img.src = e.target.result;

            img.onload = function () {
                const canvas = document.createElement('canvas');
                const ctx = canvas.getContext('2d');
                const size = Math.min(img.width, img.height);

                canvas.width = size;
                canvas.height = size;

                ctx.drawImage(img,
                    (img.width - size) / 2,
                    (img.height - size) / 2,
                    size, size,
                    0, 0, size, size);

                const dataURL = canvas.toDataURL();
                document.getElementById('profilePicture').src = dataURL;
            };
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function togglePasswordFields() {
    const passwordFields = document.getElementById('passwordFields');
    if (passwordFields.classList.contains('d-none')) {
        passwordFields.classList.remove('d-none');
    } else {
        passwordFields.classList.add('d-none');
    }
}

// Show the change password modal if there are validation errors
const showChangePasswordModal = '@(ViewData["ShowChangePasswordModal"] != null && (bool)ViewData["ShowChangePasswordModal"])';
if (showChangePasswordModal === 'True') {
    $('#changePasswordModal').modal('show');
}