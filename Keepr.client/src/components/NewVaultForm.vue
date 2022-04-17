<template>
  <div class="component">
    <h2>New Vault</h2>
    <div class="mb-3 w-100">
      <label for="name" class="form-label text-secondary">Title:</label>
      <input
        type="text"
        class="form-control"
        name="name"
        id="name"
        placeholder="Title..."
      />
    </div>
    <div class="mb-3 w-100">
      <label for="img" class="form-label text-secondary">ImgUrl:</label>
      <input
        type="text"
        class="form-control"
        name="img"
        id="img"
        placeholder="ImgUrl..."
      />
    </div>
    <div class="form-check">
      <input
        type="checkbox"
        class="form-check-input"
        name="isPrivate"
        id="isPrivate"
        value="isPrivate"
        checked
      />
      <label class="form-check-label text-secondary" for="isPrivate">
        Private?
      </label>
      <p class="form-text text-muted">Private Vaults can only be seen by you</p>
    </div>
    <div class="text-end">
      <button class="btn btn-primary text-dark text-end">Create</button>
    </div>
  </div>
</template>


<script>
import { ref } from "@vue/reactivity"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>