<template>
  <div class="modal fade" tabindex="-1">
    <div class="modal-dialog modal-dialog-centered modal-fullscreen-sm-down">
      <div class="modal-content" style="border: 4px solid #55efc4">
        <div class="modal-body">
          <div class="text-end">
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <slot name="body"></slot>
        </div>
      </div>
    </div>
  </div>
</template>
