<template>
  <div
    class="
      object-fit-cover
      rounded
      elevation-3
      my-2
      d-flex
      align-items-end
      text-light
      selectable
      select-grow
    "
    data-bs-toggle="modal"
    data-bs-target="#active-keep"
    @click="setActive"
  >
    <img :src="keep.img" alt="" class="img-fluid relative rounded" />
    <div
      class="
        d-flex
        justify-content-between
        align-items-center
        w-100
        p-2
        absolute
      "
    >
      <h4 class="m-0 text-shadow">
        {{ keep.name }}
      </h4>
      <img
        v-if="route.name != 'Profile'"
        :src="keep.creator?.picture"
        :alt="keep.creator?.name + ' picture'"
        class="img-small rounded-circle bg-light"
      />
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { AppState } from "../AppState"
import { keepsService } from "../services/KeepsService"
import { useRoute } from "vue-router"
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    return {
      route,
      backgroundImg: computed(() => `url('${props.keep.img}')`),
      async setActive() {
        try {
          await keepsService.getKeepById(props.keep.id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.bg-img {
  min-height: 20vh;
  background-blend-mode: darken;
  background-image: v-bind(backgroundImg);
  background-size: cover;
  background-position: center;
}
</style>