window.bootstrapInterop = {
    showModal: function (selector) {
        const modalElement = document.querySelector(selector);
        const modal = new bootstrap.Modal(modalElement);
        modal.show();
    }
};