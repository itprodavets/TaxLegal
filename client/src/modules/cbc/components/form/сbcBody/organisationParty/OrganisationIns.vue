<template>
	<v-data-iterator
			:items="identificationNumbers"
			v-bind:items-per-page.sync="itemsPerPage"
			hide-default-footer
	>
		<template v-slot:header>
			<div class="d-flex justify-start align-center">
				<v-btn @click="onAdd()" dense icon>
					<v-icon>mdi-plus-circle</v-icon>
				</v-btn>
				<div class="subtitle-1 text-uppercase text-center" style="width: 100%">Identification Numbers</div>
			</div>
			<v-divider class="mb-4"></v-divider>
		</template>

		<template v-slot:default="{ items }">
			<OrganisationInComponent v-for="item in items" :key="item.id"
			                         :readonly="readonly" v-bind:identificationNumber.sync="item"
			                         :countries="countries" @remove="onRemove"/>
		</template>
	</v-data-iterator>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import {In} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins/country-enum";
	import {Country} from "@/modules/country/models/dto.model";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";

	import OrganisationInComponent from "./OrganisationIn.vue";

	@Component({
		components: {
			OrganisationInComponent
		}
	})
	export default class OrganisationInsComponent extends Mixins(CountryEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;
		@Prop()
		public readonly countries!: Country[];

		@Prop({
			type: Array, default: () => {
				return [] as In[];
			}
		})
		public readonly identificationNumbers!: In[];

		public get values(): In[] {
			return this.identificationNumbers;
		}

		public set values(identificationNumbers: In[]) {
			this.$emit("update:identificationNumbers", identificationNumbers);
		}

		@Watch("identificationNumbers", {deep: true})
		public onChanged(values: In[], oldValues: In[]) {
			this.values = values;
		}

		public itemsPerPage: number = 5;

		public onAdd() {
			this.values = Object.assign(this.values, this.values.unshift({
				id: Guid.create().toString()
			} as In));
		}

		public onRemove(id: string | number) {
			this.values = this.values.filter(x => x.id !== id);
		}
	}
</script>