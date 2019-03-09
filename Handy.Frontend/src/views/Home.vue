<template>
    <div>
        <h1 class="display-1">Handy.App</h1>
        <b-card variant="secondary" bg-variant="secondary" text-variant="white">
            <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. A maiores nulla molestiae, quo placeat quam dicta, voluptate aspernatur eius labore, aliquam distinctio. In odio velit porro corrupti cum temporibus blanditiis!</p>
            <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. A maiores nulla molestiae, quo placeat quam dicta, voluptate aspernatur eius labore, aliquam distinctio. In odio velit porro corrupti cum temporibus blanditiis!</p>
        </b-card>
        <section class="homepage-section">
            <h2 class="display-4">Latest Notes</h2>
            <b-card-group deck>
                <NoteCard v-for="note in note.notes" :key="note.id" :note="note" />
            </b-card-group>
        </section>
        <section class="homepage-section">
            <h2 class="display-4">Latest Reminders</h2>
            <b-card-group deck>
                <b-card title="@ 18 March 2019, 19:53" img-top>
                    <b-card-text>
                    Some quick example text to build on the card and make up the bulk of the card's content.
                    </b-card-text>
                    <b-button-group>
                        <b-button href="#" variant="warning" v-b-tooltip.hover.bottom title="Edit Reminder"><v-icon name="edit" /></b-button>
                        <b-button href="#" variant="dark" v-b-tooltip.hover.bottom title="Details"><v-icon name="arrow-right" /></b-button>
                        <b-button href="#" variant="info" v-b-tooltip.hover.bottom title="Reassign to another time"><v-icon name="clock" /></b-button>
                        <b-button href="#" variant="secondary" v-b-tooltip.hover.bottom title="Turn Off"><v-icon name="power-off" /></b-button>
                        <b-button href="#" variant="danger" v-b-tooltip.hover.bottom title="Delete Reminder"><v-icon name="trash-alt" /></b-button>
                    </b-button-group>
                </b-card>
                <b-card title="@ 18 March 2019, 19:53" img-top>
                    <b-card-text>
                    Some quick example text to build on the card and make up the bulk of the card's content.
                    </b-card-text>
                </b-card>
                <b-card title="@ 18 March 2019, 19:53" img-top>
                    <b-card-text>
                    Some quick example text to build on the card and make up the bulk of the card's content.
                    </b-card-text>
                </b-card>
            </b-card-group>
        </section>
    </div>
</template>

<script>
import NoteCard from '@/components/notes/NoteCard';
import { mapState, mapActions } from 'vuex';

export default {
    components: {
        NoteCard
    },
    computed: mapState(['note']),
    created() {
        this.fetchNotes();
    },
    methods: {
        ...mapActions('note', ['fetchNotes'])
    } 
}
</script>


<style lang="scss" scoped>
.homepage-section {
    margin: 2rem 0;
}

h1, h2 {
    text-align: center;
}
</style>
