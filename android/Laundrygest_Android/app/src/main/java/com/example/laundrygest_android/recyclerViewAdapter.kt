package com.example.laundrygest_android

import android.content.Context
import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.laundrygest_android.data.CollectionDto

class recyclerViewAdapter(val list: List<CollectionDto>) :
    RecyclerView.Adapter<recyclerViewAdapter.ViewHolder>() {
    lateinit var context: Context

    class ViewHolder(val view: View) : RecyclerView.ViewHolder(view) {
        val numCollection = view.findViewById<TextView>(R.id.collection_code_text)
        val dateCollection = view.findViewById<TextView>(R.id.collection_date_text)
        val priceCollection = view.findViewById<TextView>(R.id.due_total_text)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val layout = LayoutInflater.from(parent.context)
        this.context = parent.context
        return ViewHolder(layout.inflate(R.layout.cardview, parent, false))
    }

    override fun getItemCount() = list.size

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        holder.numCollection.setText(list[position].number)
        holder.dateCollection.setText(list[position].dueDate.toString())
        holder.priceCollection.setText(list[position].dueTotal.toString())
        holder.itemView.setOnClickListener {
            var intent = Intent(context, CollectionDetailActivity::class.java)
            intent.putExtra("collection", list[position])
            holder.itemView.context.startActivity(intent)
        }
    }

}